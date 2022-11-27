using FluentValidation;

namespace BildMlue.Application.DTO.Incidents;

public record EndIncidentInDto
{
    public required string Email { get; init; }
}

public class EndIncidentInDtoValidator : AbstractValidator<EndIncidentInDto>
{
    public EndIncidentInDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}