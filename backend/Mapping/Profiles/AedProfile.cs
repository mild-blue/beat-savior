using AutoMapper;
using BildMlue.Application.DTO.Aed;
using BildMlue.Domain.Entities;

namespace BildMlue.Infrastructure.Mapping.Profiles;

public class AedProfile : Profile
{
    public AedProfile()
    {
        CreateMap<AutomatedExternalDefibrillator, AedOutDto>();
    }
}