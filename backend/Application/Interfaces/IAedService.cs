using BildMlue.Application.DTO.Aed;

namespace BildMlue.Application.Interfaces;

public interface IAedService
{
    Task<List<AedOutDto>> GetAll(int count, double latitude, double longitude);
}