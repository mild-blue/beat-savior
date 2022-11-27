using FluentValidation;

namespace BildMlue.Application.DTO.Incidents;

public record AcceptIncidentInDto
{
    public required string Email { get; init; }
    public required bool IsCprCapable { get; init; }
}

public class AcceptIncidentInDtoValidator : AbstractValidator<AcceptIncidentInDto>
{
    public AcceptIncidentInDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
    }
}