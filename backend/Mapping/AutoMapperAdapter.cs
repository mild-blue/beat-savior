using BildMlue.Application.Interfaces;

namespace BildMlue.Infrastructure.Mapping;

public class AutoMapperAdapter : IMapper
{
    private readonly AutoMapper.IMapper _mapper;

    public AutoMapperAdapter(AutoMapper.IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) =>
        _mapper.Map<TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) =>
        _mapper.Map(source, destination);

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source) =>
        _mapper.ProjectTo<TDestination>(source);
}