using System.Security.Claims;
using BookStore.Domain.Entites.Books;
using BookStore.Domain.Entites.Users;
using BookStore.Domain.Enums;
using BookStore.Service.DTOs.Users;
using BookStore.Service.Helpers;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        var isRoleUser = Enum.TryParse(HttpContext.User.FindFirst(ClaimTypes.Role)?.Value, 
            false, out UserRole role);

        if (isRoleUser && role == UserRole.SuperAdmin)
            return Ok(await _userService.CreateAsync(dto, UserRole.Admin));

        return Ok(await _userService.CreateAsync(dto));
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login(string login, string password)
    {
        return Ok(new {Token = await _userService.LoginAsync(login, password)});
    }
    
    [HttpGet, Authorize]
    public async Task<ActionResult<User>> GetUserInfo()
    {
        return Ok(await _userService.GetAsync(user => user.Id == HttpContextHelper.UserId));
    }
}