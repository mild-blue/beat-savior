using BildMlue.Application.Interfaces;
using BildMlue.Domain.Events;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BildMlue.Infrastructure.Firebase;

public class FirebaseNotificationService : INotificationService
{
    private readonly ILogger<FirebaseNotificationService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public FirebaseNotificationService(ILogger<FirebaseNotificationService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        _logger.LogInformation("Initializing Firebase...");
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json");
        var options = new AppOptions
        {
            Credential = GoogleCredential.FromFile(path)
        };
        FirebaseApp.Create(options);
        _logger.LogInformation("Firebase initialized");
    }

    public async Task SendMessage(string id, EventBase @event)
    {
        using var scope = _serviceProvider.CreateScope();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        _logger.LogInformation("Sending message to {Id}", id);
        var data = mapper.Map<Dictionary<string, string>>(@event);

        var messageToSend = new Message
        {
            Token = id,
            Notification = new Notification
            {
                Title = @event.Title,
                Body = @event.Body
            },
            Data = data
        };

        try
        {
            await FirebaseMessaging.DefaultInstance.SendAsync(messageToSend);
            _logger.LogInformation("Message sent to {Id}", id);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Error sending message to {Id}", id);
        }
    }

    public async Task SendMessage(IEnumerable<string> ids, EventBase @event)
    {
        foreach (var id in ids)
        {
            await SendMessage(id, @event);
        }
    }
}