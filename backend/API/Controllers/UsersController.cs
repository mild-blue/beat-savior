using BildMlue.Application.DTO.Users;
using BildMlue.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BildMlue.API.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) =>
        _userService = userService;

    /// <summary>
    /// Get all registered users
    /// </summary>
    [HttpGet]
    public Task<List<UserOutDto>> GetUsers() =>
        _userService.GetAll();

    /// <summary>
    /// Register new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(UserRegisterInDto data) =>
        await _userService.Register(data) ? Ok() : Conflict();

    /// <summary>
    /// Update existing user
    /// </summary>
    [HttpPut("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string email, UserUpdateInDto data) =>
        await _userService.Update(email, data) ? Ok() : NotFound();

    /// <summary>
    /// Delete user - for testing only
    /// </summary>
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUser(string email) =>
        await _userService.Delete(email) ? Ok() : NotFound();
}