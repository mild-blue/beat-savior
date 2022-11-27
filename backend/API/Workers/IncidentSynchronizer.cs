using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using BildMlue.Infrastructure.FHIR;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.EntityFrameworkCore;
using Location = Hl7.Fhir.Model.Location;
using Task = System.Threading.Tasks.Task;

namespace BildMlue.API.Workers;

/// <summary>
/// Writes reported incidents to FHIR.
/// </summary>
public class IncidentSynchronizer : BackgroundService
{
    private readonly ILogger<IncidentSynchronizer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly FhirClient _client;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(5));

    public IncidentSynchronizer(ILogger<IncidentSynchronizer> logger, IServiceProvider serviceProvider,
        FhirClientFactory factory)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _client = factory.Client;
    }

    private async Task<Encounter> CreateEncounter(Incident incident)
    {
        var encounter = new Encounter
        {
            Class = new Coding {Code = "EMER", System = "https://terminology.hl7.org/CodeSystem/v3-ActCode"},
            Status = incident.IsEnded ? Encounter.EncounterStatus.Finished : Encounter.EncounterStatus.InProgress,
        };

        var location = await _client.CreateAsync(new Location
        {
            Position = new Location.PositionComponent
            {
                Latitude = (decimal) incident.Latitude,
                Longitude = (decimal) incident.Longitude
            }
        });

        encounter.Location = new List<Encounter.LocationComponent>
        {
            new()
            {
                Location = new ResourceReference(location.ResourceBase.ToString())
            }
        };
        return await _client.CreateAsync(encounter);
    }

    private async Task<Encounter> UpdateEncounter(Incident incident)
    {
        var encounter = (await _client.SearchAsync<Encounter>(new SearchParams("id", incident.ExternalId)))
            .Entry
            .Select(e => e.Resource)
            .OfType<Encounter>()
            .FirstOrDefault();

        if (encounter is null)
        {
            return await CreateEncounter(incident);
        }

        encounter.Status = incident.IsEnded ? Encounter.EncounterStatus.Finished : Encounter.EncounterStatus.InProgress;
        return await _client.UpdateAsync(encounter);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            _logger.LogInformation("Synchronizing incidents with FHIR");
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
            var incidents = await context.Set<Incident>().ToListAsync(stoppingToken);
            _logger.LogInformation("Found {Count} incidents", incidents.Count);
            foreach (var incident in incidents)
            {
                try
                {
                    if (incident.ExternalId is null)
                    {
                        _logger.LogInformation("Creating encounter for incident {Id}", incident.Id);
                        var encounter = await CreateEncounter(incident);
                        incident.ExternalId = encounter.Id;
                        await context.SaveChanges(stoppingToken);
                        _logger.LogInformation("Created encounter {Id} for incident {IncidentId}", encounter.Id,
                            incident.Id);
                    }
                    else
                    {
                        _logger.LogInformation("Updating encounter {Id} for incident {IncidentId}", incident.ExternalId,
                            incident.Id);
                        await UpdateEncounter(incident);
                        _logger.LogInformation("Updated encounter {Id} for incident {IncidentId}", incident.ExternalId,
                            incident.Id);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while synchronizing incident {Id}", incident.Id);
                }
            }
        } while (await _timer.WaitForNextTickAsync(stoppingToken));
    }
}