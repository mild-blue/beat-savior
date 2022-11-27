using BildMlue.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BildMlue.Infrastructure.Firebase;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddFirebase(this IServiceCollection services)
    {
        services.AddSingleton<INotificationService, FirebaseNotificationService>();
        return services;
    }
}