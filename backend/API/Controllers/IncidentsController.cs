using BildMlue.Application.DTO.Incidents;
using BildMlue.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BildMlue.API.Controllers;

public class IncidentsController : ApiController
{
    private readonly IIncidentService _incidentService;

    public IncidentsController(IIncidentService incidentService) =>
        _incidentService = incidentService;

    /// <summary>
    /// Get incident by id
    /// </summary>
    [HttpGet("{id}")]
    public Task<IncidentOutDto> GetDetail(Guid id) =>
        _incidentService.GetDetail(id);

    /// <summary>
    /// Report an incident
    /// </summary>
    [HttpPost("report")]
    public Task<IncidentOutDto> Report(CreateIncidentInDto incident) =>
        _incidentService.Report(incident);

    /// <summary>
    /// First responder accepts an incident
    /// </summary>
    [HttpPost("{id}/accept")]
    public Task<AssignmentOutDto> Accept(Guid id, AcceptIncidentInDto data) =>
        _incidentService.Accept(id, data);

    /// <summary>
    /// Ends the rescue operation.
    /// </summary>
    [HttpPost("{id}/end")]
    public Task<AssignmentOutDto> End(Guid id, EndIncidentInDto data) =>
        _incidentService.EndIncident(id, data);
}