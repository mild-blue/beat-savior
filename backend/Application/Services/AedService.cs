using BildMlue.Application.DTO.Aed;
using BildMlue.Application.Extensions;
using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BildMlue.Application.Services;

public class AedService : IAedService
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public AedService(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public Task<List<AedOutDto>> GetAll(int count, double latitude, double longitude) =>
        _context
            .Set<AutomatedExternalDefibrillator>()
            .AsNoTracking()
            .OrderBy(aed =>
                (aed.Latitude - latitude) * (aed.Latitude - latitude) +
                (aed.Longitude - longitude) * (aed.Longitude - longitude))
            .Take(count)
            .ProjectTo<AedOutDto>(_mapper)
            .ToListAsync();
}