using BildMlue.Domain.Enums;

namespace BildMlue.Domain.Entities;

public class Incident : AppEntity
{
    private readonly HashSet<FirstResponder> _firstResponders = new();

    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required DateTime OccurredAt { get; init; }
    public bool IsEnded { get; private set; }

    /// <summary>
    /// Id in external system (FHIR)
    /// </summary>
    public string? ExternalId { get; set; }

    /// <summary>
    /// Whether the person who called the emergency services is CPR capable.
    /// </summary>
    public required bool CallerIsCprCapable { get; init; }

    /// <summary>
    /// Automatic external defibrillator that was nearest during the incident.
    /// </summary>
    public required AutomatedExternalDefibrillator NearestAED { get; init; }

    /// <summary>
    /// First responders that accepted the incident.
    /// </summary>
    public HashSet<FirstResponder> FirstResponders { get; private set; } = new();

    public FirstResponder? DefibrillatorCarrier { get; private set; }

    /// <summary>
    /// First responder accepts the incident.
    /// </summary>
    public Assignment AcceptRequest(FirstResponder responder)
    {
        FirstResponders.Add(responder);
        var assignment = FirstResponders.Count switch
        {
            1 when responder.IsCprCapable => new Assignment
            {
                Type = AssignmentType.Resuscitation,
                PatientLocation = new Location(Latitude, Longitude),
                AssignmentLocation = new Location(Latitude, Longitude),
                Incident = this
            },
            1 when !responder.IsCprCapable => new Assignment
            {
                Type = AssignmentType.DefibrillatorPickup,
                PatientLocation = new Location(Latitude, Longitude),
                AssignmentLocation = new Location(NearestAED.Latitude, NearestAED.Longitude),
                Incident = this
            },
            2 => new Assignment
            {
                Type = AssignmentType.DefibrillatorPickup,
                PatientLocation = new Location(Latitude, Longitude),
                AssignmentLocation = new Location(NearestAED.Latitude, NearestAED.Longitude),
                Incident = this
            },
            _ => new Assignment
            {
                Type = AssignmentType.Assisting,
                PatientLocation = new Location(Latitude, Longitude),
                AssignmentLocation = new Location(Latitude, Longitude),
                Incident = this
            }
        };

        if (assignment.Type == AssignmentType.DefibrillatorPickup)
        {
            DefibrillatorCarrier = responder;
        }

        return assignment;
    }

    /// <summary>
    /// Ends the rescue operation.
    /// </summary>
    public Assignment EndIncident(FirstResponder responder)
    {
        var assignment = responder.Email == DefibrillatorCarrier?.Email
            ? new Assignment
            {
                Type = AssignmentType.DefibrillatorReturn,
                PatientLocation = new Location(Latitude, Longitude),
                AssignmentLocation = new Location(NearestAED.Latitude, NearestAED.Longitude),
                Incident = this
            }
            : new Assignment
            {
                Type = AssignmentType.None,
                PatientLocation = new Location(Latitude, Longitude),
                AssignmentLocation = new Location(Latitude, Longitude),
                Incident = this
            };

        IsEnded = true;
        return assignment;
    }
}