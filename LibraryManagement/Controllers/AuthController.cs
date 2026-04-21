using Core.DTO;
using Core.Interfaces;
using LibraryManagement.Application.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var user = await _authService.ValidateUserAsync(request.Username, request.Password);

        return Ok(new { Token = user.Token,Error =user.Error });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignupRequestDto request)
    {
        var user = await _authService.RegisterUserAsync(request.Username, request.Email, request.Password);

        if (user == null)
            return BadRequest("Username or email already exists");

        return Ok("User registered successfully");
    }
}
