using Core.DTO;
using Core.Interfaces;
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
        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Core.DTO.RegisterRequest request)
    {
        var user = await _authService.RegisterUserAsync(request.Username, request.Email, request.Password);

        if (user == null)
            return BadRequest("Username or email already exists");

        return Ok("User registered successfully");
    }
}
