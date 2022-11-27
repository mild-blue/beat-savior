using BildMlue.Infrastructure.Persistence.Postgre;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Pathoschild.Http.Client;
using Xunit;
using Xunit.Abstractions;

namespace API.Tests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected readonly IClient Client;
    protected readonly HttpClient RawClient;
    protected readonly IServiceProvider Services;
    protected readonly DatabaseFixture Fixture;
    protected readonly ITestOutputHelper Output;

    protected IntegrationTestBase(DatabaseFixture fixture, ITestOutputHelper output)
    {
        Fixture = fixture;
        Output = output;
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // remove all background jobs
                    var hostedServices = services
                        .Where(x => x.ServiceType.IsAssignableTo(typeof(IHostedService)))
                        .ToList();
                    foreach (var hostedService in hostedServices)
                    {
                        services.Remove(hostedService);
                    }

                    // remove the existing context configuration
                    var descriptor =
                        services.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    var configuration = services
                        .BuildServiceProvider()
                        .GetRequiredService<IConfiguration>();

                    var cs = configuration
                        .GetConnectionString("db")!
                        .Replace("petricords", fixture.DbName);

                    services.AddPersistence(cs, true);
                });

                builder.ConfigureAppConfiguration((_, configurationBuilder) =>
                {
                    ConfigureOptions(configurationBuilder);
                });
            });
        RawClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        Client = new FluentClient(RawClient.BaseAddress, RawClient);
        Services = factory.Services;
    }

    protected virtual void ConfigureOptions(IConfigurationBuilder configurationBuilder)
    {
    }

    public virtual Task InitializeAsync() =>
        Task.CompletedTask;

    public virtual async Task DisposeAsync()
    {
        using var scope = Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();

        await using var connection = new NpgsqlConnection(context.Database.GetConnectionString());
        await connection.OpenAsync();
        await Fixture.Respawner!.ResetAsync(connection);
    }
}