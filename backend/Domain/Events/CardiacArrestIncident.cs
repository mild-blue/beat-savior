namespace BildMlue.Domain.Events;

public class CardiacArrestIncident : EventBase
{
    public CardiacArrestIncident() : base("Cardiac arrest incident", "A person has suffered a cardiac arrest")
    {
    }

    public required double Latitude { get; init; }
    public required double Longitude { get; init; }

    /// <summary>
    /// The time the incident was reported (in UTC).
    /// </summary>
    public required DateTime OccurredAt { get; init; }
}