using FluentValidation;

namespace BildMlue.Application.DTO.Todo;

public record CreateTodoInDto
{
    public required string Title { get; init; }
}

public class CreateTodoInDtoValidator : AbstractValidator<CreateTodoInDto>
{
    public CreateTodoInDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}