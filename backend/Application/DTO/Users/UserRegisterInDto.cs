using FluentValidation;

namespace BildMlue.Application.DTO.Users;

public record UserRegisterInDto
{
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required bool IsCprCapable { get; init; }
}

public class UserRegisterInDtoValidator : AbstractValidator<UserRegisterInDto>
{
    public UserRegisterInDtoValidator()
    {
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}