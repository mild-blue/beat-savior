using BildMlue.Infrastructure.Persistence.Postgre;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Xunit;

namespace API.Tests;

public class DatabaseFixture : IAsyncLifetime
{
    public string DbName { get; } = Guid.NewGuid().ToString().Replace("-", "");

    public Respawner? Respawner { get; private set; }

    private IServiceProvider PrepareServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", false)
            .AddEnvironmentVariables()
            .Build();

        var cs = GetModifiedConnectionString(configuration);

        var services = new ServiceCollection();
        services.AddPersistence(cs, true);
        return services.BuildServiceProvider();
    }

    public async Task InitializeAsync()
    {
        using var scope = PrepareServices().CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();
        // TODO await context.Seed();

        await using var connection = new NpgsqlConnection(context.Database.GetConnectionString());
        await connection.OpenAsync();
        Respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            SchemasToInclude = new[]
            {
                "public"
            },
            DbAdapter = DbAdapter.Postgres
        });
    }

    public Task DisposeAsync()
    {
        using var scope = PrepareServices().CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        return context.Database.EnsureDeletedAsync();
    }

    public string GetModifiedConnectionString(IConfiguration configuration) =>
        configuration
            .GetConnectionString("db")!
            .Replace("hackathon", DbName);
}