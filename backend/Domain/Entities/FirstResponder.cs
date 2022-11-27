namespace BildMlue.Domain.Entities;

public record FirstResponder
{
    public required string Email { get; init; }
    public required bool IsCprCapable { get; init; }
}