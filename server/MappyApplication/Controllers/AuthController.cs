using MappyApplication.Contract;
using MappyApplication.Models.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MappyApplication.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;

    public AuthController(IAuthManager authManager)
    {
        _authManager = authManager;
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<AuthResponse> Register([FromBody] AuthRegister authRegister)
    {
        return await _authManager.Register(authRegister);
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<AuthResponse> Login([FromBody] AuthLogin authLogin)
    {
        return await _authManager.Login(authLogin);
    }

    [HttpPost]
    [Route("refresh-token")]
    [Authorize]
    public async Task<AuthResponse> RefreshToken()
    {
        return await _authManager.Refresh();
    }


    [HttpGet]
    [Route("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authManager.Logout();

        return Ok(new { message = "You have been logged out." });
    }
}