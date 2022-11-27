using BildMlue.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BildMlue.Infrastructure.Persistence.Postgre;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration, string connectionStringName = "db") =>
        services.AddPersistence(configuration.GetConnectionString(connectionStringName) ?? "");

    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString,
        bool sensitiveDataLoggingEnabled = false)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled);
        });

        return services;
    }
}