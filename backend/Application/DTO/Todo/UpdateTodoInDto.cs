using FluentValidation;

namespace BildMlue.Application.DTO.Todo;

public record UpdateTodoInDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required bool IsDone { get; init; }
}

public class UpdateTodoInDtoValidator : AbstractValidator<UpdateTodoInDto>
{
    public UpdateTodoInDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}