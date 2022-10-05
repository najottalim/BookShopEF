using BookStore.Domain.Entites.Users;
using BookStore.Service.DTOs.Users;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(UserForCreationDto dto)
    {
        return Ok(await _userService.CreateAsync(dto));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string login, string password)
    {
        return Ok(new {Token = await _userService.LoginAsync(login, password)});
    }
}