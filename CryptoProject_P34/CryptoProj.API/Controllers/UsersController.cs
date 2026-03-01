using CryptoProj.Domain.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CryptoProj.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] int id)
    {
        var user = await _usersService.GetById(id);
        return Ok(user);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var user = await _usersService.Register(request);
        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var user = await _usersService.Login(request);
        return Ok(user);
    }

    // 1. redirect to Google
    [HttpGet("google")]
    public IActionResult GoogleLogin()
    {
        var props = new AuthenticationProperties
        {
            RedirectUri = "/api/v1/users/google-callback"
        };

        return Challenge(props, "Google");
    }

    // 2. callback
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync("Google");

        if (!result.Succeeded)
            return BadRequest("Google auth failed");

        var googleId = result.Principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var email = result.Principal.FindFirst(ClaimTypes.Email)!.Value;
        var name = result.Principal.FindFirst(ClaimTypes.Name)!.Value;

        var user = await _usersService.LoginWithGoogle(googleId, email, name);

        return Ok(user); // тут твій JWT
    }
}