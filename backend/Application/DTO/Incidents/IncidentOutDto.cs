using BildMlue.Domain.Entities;

namespace BildMlue.Application.DTO.Incidents;

public record IncidentOutDto
{
    public required Guid Id { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required DateTime OccurredAt { get; init; }
    public required AutomatedExternalDefibrillator NearestAED { get; init; }
}