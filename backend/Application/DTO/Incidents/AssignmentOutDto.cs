using BildMlue.Application.DTO.Aed;
using BildMlue.Domain.Entities;
using BildMlue.Domain.Enums;

namespace BildMlue.Application.DTO.Incidents;

public class AssignmentOutDto
{
    /// <summary>
    /// Type of the assignment.
    /// </summary>
    public required AssignmentType Type { get; init; }

    /// <summary>
    /// Location of the patient.
    /// </summary>
    public required Location PatientLocation { get; init; }

    /// <summary>
    /// Location of the nearest assisted (defibrillator or patient or defibrillator return point).
    /// </summary>
    public required Location AssignmentLocation { get; init; }

    /// <summary>
    /// Automatic external defibrillator that was nearest during the incident.
    /// </summary>
    public required AedOutDto NearestAED { get; init; }
}