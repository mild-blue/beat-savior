using BildMlue.Application.Interfaces;

namespace BildMlue.Application.Extensions;

public static class MapperExtensions
{
    /// <summary>
    /// Project the input queryable.
    /// </summary>
    /// <param name="source">Queryable source</param>
    /// <param name="mapper">Mapper to be used for projection.</param>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <returns></returns>
    public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IMapper mapper) =>
        mapper.ProjectTo<TDestination>(source);
}