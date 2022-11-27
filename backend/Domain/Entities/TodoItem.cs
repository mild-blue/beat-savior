namespace BildMlue.Domain.Entities;

public class TodoItem : AppEntity
{
    public required string Title { get; set; }
    public string Description { get; set; } = "";
    public bool IsDone { get; set; }
}