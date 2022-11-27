namespace BildMlue.Application.DTO.Todo;

public record TodoListOutDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required bool IsDone { get; init; }
}