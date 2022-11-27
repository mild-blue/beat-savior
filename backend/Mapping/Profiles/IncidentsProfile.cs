using AutoMapper;
using BildMlue.Application.DTO.Incidents;
using BildMlue.Domain.Entities;
using BildMlue.Domain.Events;

namespace BildMlue.Infrastructure.Mapping.Profiles;

public class IncidentsProfile : Profile
{
    public IncidentsProfile()
    {
        CreateMap<CardiacArrestIncident, Dictionary<string, string>>();
        CreateMap<Incident, IncidentOutDto>();
        CreateMap<Assignment, AssignmentOutDto>()
            .ForMember(x => x.NearestAED, x => x.MapFrom(y => y.Incident.NearestAED));
    }
}