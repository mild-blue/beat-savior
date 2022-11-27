namespace BildMlue.Application.Exceptions;

public class NotFoundException<TEntity> : ApplicationException
{
    public NotFoundException(string id) : base($"{typeof(TEntity).Name} with id {id} not found")
    {
    }

    public NotFoundException(Guid id) : this(id.ToString())
    {
    }
}