using BildMlue.Application.DTO.Incidents;

namespace BildMlue.Application.Interfaces;

public interface IIncidentService
{
    /// <summary>
    /// Get incident by id
    /// </summary>
    Task<IncidentOutDto> GetDetail(Guid id);

    /// <summary>
    /// Report an incident
    /// </summary>
    Task<IncidentOutDto> Report(CreateIncidentInDto incident);

    /// <summary>
    /// First responder accepts an incident
    /// </summary>
    /// <param name="id">Incident Id</param>
    /// <param name="data"></param>
    Task<AssignmentOutDto> Accept(Guid id, AcceptIncidentInDto data);

    /// <summary>
    /// Ends the rescue operation.
    /// </summary>
    Task<AssignmentOutDto> EndIncident(Guid id, EndIncidentInDto data);
}