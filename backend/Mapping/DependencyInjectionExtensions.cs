using BildMlue.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BildMlue.Infrastructure.Mapping;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjectionExtensions));
        services.AddScoped<IMapper, AutoMapperAdapter>();
        return services;
    }
}