namespace BildMlue.Domain.Entities;

public abstract class AppEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return Id.Equals(((AppEntity) obj).Id);
    }

    public override int GetHashCode() =>
        Id.GetHashCode();
}