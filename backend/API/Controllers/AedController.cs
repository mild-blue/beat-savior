using BildMlue.Application.DTO.Aed;
using BildMlue.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace BildMlue.API.Controllers;

public class AedController : ApiController
{
    private readonly IAedService _aedService;

    public AedController(IAedService aedService) =>
        _aedService = aedService;

    /// <summary>
    /// Get all AEDs
    /// </summary>
    /// <param name="count">Number of nearest AEDs to return</param>
    [HttpGet]
    public Task<List<AedOutDto>> GetAll(
        int count = 100,
        double latitude = IKEM.Latitude,
        double longitude = IKEM.Longitude
    ) => _aedService.GetAll(count, latitude, longitude);
}