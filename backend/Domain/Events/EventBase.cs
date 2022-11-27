namespace BildMlue.Domain.Events;

public abstract class EventBase
{
    protected EventBase(string title, string body)
    {
        Title = title;
        Body = body;
    }

    public string Title { get; }
    public string Body { get; }
}