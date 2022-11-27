namespace BildMlue.Application.DTO.Incidents;

public record CreateIncidentInDto
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }

    /// <summary>
    /// Whether the person who called the emergency services is CPR capable.
    /// </summary>
    public required bool CallerIsCprCapable { get; init; }
}