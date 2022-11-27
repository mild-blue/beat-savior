namespace BildMlue.Application.DTO.Users;

public record UserOutDto
{
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string? FirebaseToken { get; init; }
    public required bool IsCprCapable { get; init; }
}