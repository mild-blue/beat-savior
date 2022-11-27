using FluentValidation;

namespace BildMlue.Application.DTO.Users;

public record UserUpdateInDto
{
    public required string PhoneNumber { get; init; }
    public required string Name { get; init; }
    public string? FirebaseToken { get; init; }
    public required bool IsCprCapable { get; init; }
}

public class UserUpdateInDtoValidator : AbstractValidator<UserUpdateInDto>
{
    public UserUpdateInDtoValidator()
    {
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.FirebaseToken).NotEmpty().When(x => x.FirebaseToken is not null);
    }
}