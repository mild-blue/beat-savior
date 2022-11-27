using BildMlue.Domain.Enums;

namespace BildMlue.Domain.Entities;

public record Assignment
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
    /// Incident related to the assignment.
    /// </summary>
    public required Incident Incident { get; init; }
}