using BildMlue.Application.DTO.Users;
using BildMlue.Domain.Entities;

namespace BildMlue.Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Get all registered users
    /// </summary>
    Task<List<UserOutDto>> GetAll();

    /// <summary>
    /// Find first responder by email
    /// </summary>
    Task<FirstResponder> Get(string email);

    /// <summary>
    /// Register new user
    /// </summary>
    /// <returns>False if user already exists</returns>
    Task<bool> Register(UserRegisterInDto data);

    /// <summary>
    /// Update existing user
    /// </summary>
    Task<bool> Update(string email, UserUpdateInDto data);

    /// <summary>
    /// Delete user
    /// </summary>
    Task<bool> Delete(string id);
}