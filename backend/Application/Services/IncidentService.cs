using BildMlue.Application.DTO.Incidents;
using BildMlue.Application.Exceptions;
using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BildMlue.Application.Services;

public class IncidentService : IIncidentService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;
    private readonly IAedService _aedService;

    public IncidentService(IAppDbContext context, IMapper mapper, INotificationService notificationService,
        IUserService userService, IAedService aedService)
    {
        _context = context;
        _mapper = mapper;
        _notificationService = notificationService;
        _userService = userService;
        _aedService = aedService;
    }

    public async Task<IncidentOutDto> GetDetail(Guid id)
    {
        var incident = await _context
            .Set<Incident>()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException<Incident>(id);

        return _mapper.Map<IncidentOutDto>(incident);
    }

    public async Task<IncidentOutDto> Report(CreateIncidentInDto incident)
    {
        var aeds = await _aedService.GetAll(1, incident.Latitude, incident.Longitude);
        var aed = await _context
            .Set<AutomatedExternalDefibrillator>()
            .FirstAsync(x => x.Id == aeds.Single().Id);

        var entity = new Incident
        {
            Latitude = incident.Latitude,
            Longitude = incident.Longitude,
            OccurredAt = DateTime.UtcNow,
            NearestAED = aed,
            CallerIsCprCapable = incident.CallerIsCprCapable
        };
        _context.Set<Incident>().Add(entity);
        await _context.SaveChanges();

        /* 
        var responders = await _userService.GetAll();
        var @event = new CardiacArrestIncident
        {
            Latitude = incident.Latitude,
            Longitude = incident.Longitude,
            OccurredAt = DateTime.UtcNow
        };

        var tokens = responders
            .Select(x => x.FirebaseToken)
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => x!);

        await _notificationService.SendMessage(tokens, @event);
        */

        return await GetDetail(entity.Id);
    }

    public async Task<AssignmentOutDto> Accept(Guid id, AcceptIncidentInDto data)
    {
        var incident = await _context
            .Set<Incident>()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException<Incident>(id);

        var fr = new FirstResponder()
        {
            Email = data.Email,
            IsCprCapable = data.IsCprCapable
        };
        var assignment = incident.AcceptRequest(fr);
        _context.Set<Incident>().Update(incident);
        await _context.SaveChanges();

        return _mapper.Map<AssignmentOutDto>(assignment);
    }

    public async Task<AssignmentOutDto> EndIncident(Guid id, EndIncidentInDto data)
    {
        var incident = await _context
            .Set<Incident>()
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException<Incident>(id);

        var fr = await _userService.Get(data.Email);
        var assignment = incident.EndIncident(fr);
        _context.Set<Incident>().Update(incident);
        await _context.SaveChanges();

        return _mapper.Map<AssignmentOutDto>(assignment);
    }
}