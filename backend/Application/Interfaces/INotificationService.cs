using BildMlue.Domain.Events;

namespace BildMlue.Application.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Sends a message to the specified device.
    /// </summary>
    /// <param name="id">Device ID.</param>
    /// <param name="event">Message to be sent.</param>
    Task SendMessage(string id, EventBase @event);

    /// <summary>
    /// Sends a message to the specified devices.
    /// </summary>
    /// <param name="ids">Device IDs.</param>
    /// <param name="event">Message to be sent.</param>
    Task SendMessage(IEnumerable<string> ids, EventBase @event);
}