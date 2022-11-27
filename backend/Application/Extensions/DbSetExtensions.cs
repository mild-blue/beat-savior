using BildMlue.Application.Exceptions;
using BildMlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BildMlue.Application.Extensions;

public static class DbSetExtensions
{
    /// <summary>
    /// Tries to find an entity by id or throws an exception if not found.
    /// By default does not include related data. Good for establishing relations.
    /// </summary>
    public static async ValueTask<TEntity> FindOrThrow<TEntity>(this DbSet<TEntity> set, Guid id)
        where TEntity : AppEntity
    {
        var entity = await set.FindAsync(id);
        return entity ?? throw new NotFoundException<TEntity>(id);
    }

    /// <summary>
    /// Tries to find an entity by id or throws an exception if not found.
    /// Includes related data. Good for returning data to the client.
    /// </summary>
    public static async Task<TEntity> GetDetailOrThrow<TEntity>(this IQueryable<TEntity> source, Guid id)
        where TEntity : AppEntity
    {
        var entity = await source.FirstOrDefaultAsync(x => x.Id == id);
        return entity ?? throw new NotFoundException<TEntity>(id);
    }
}