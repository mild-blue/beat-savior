using BildMlue.Infrastructure.FHIR;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Mvc;

namespace BildMlue.API.Controllers;

public class PatientsController : ApiController
{
    private readonly FhirClientFactory _fhirClientFactory;

    public PatientsController(FhirClientFactory fhirClientFactory) => 
        _fhirClientFactory = fhirClientFactory;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var client = _fhirClientFactory.Client;
        var patients = (await client.SearchAsync<Patient>())
            .Entry
            .Select(x => x.Resource)
            .OfType<Patient>()
            .ToList();

        var dtos = patients.Select(x => new PatientDto(x.Id, string.Join(" ", x.Name)));
        return Ok(dtos);
    }
}

public record PatientDto(string Id, string Name);