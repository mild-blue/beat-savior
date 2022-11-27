using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BildMlue.Infrastructure.FHIR.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddFhir(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<FhirOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();

        services.AddSingleton<FhirClientFactory>();
        return services;
    }
}