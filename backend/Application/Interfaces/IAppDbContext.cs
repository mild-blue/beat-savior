using Microsoft.EntityFrameworkCore;

namespace BildMlue.Application.Interfaces;

public interface IAppDbContext
{
    /// <summary>
    /// Get a DbSet for a given entity type.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    /// <summary>
    /// Persist all changes made on DbSets.
    /// </summary>
    /// <returns></returns>
    Task SaveChanges(CancellationToken cancellationToken = default);
}