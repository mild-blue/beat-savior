using BildMlue.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BildMlue.Infrastructure.AED;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAedImporter, AedImporter>();
        return services;
    }
}