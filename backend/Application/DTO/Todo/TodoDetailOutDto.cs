namespace BildMlue.Application.DTO.Todo;

public record TodoDetailOutDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required bool IsDone { get; init; }
}