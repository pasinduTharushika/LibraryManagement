using Core.Entities;
using Core.Interfaces;
using LibraryManagement.Application.DTO;
using LibraryManagement.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService 
{
    private readonly IConfiguration _configuration;
    private readonly IAuthRepository _authRepository;
    

    public AuthService(IConfiguration configuration, IAuthRepository authRepository)
    {
        _configuration = configuration;
        _authRepository = authRepository;
    }

    // Validate user using MD5 hashing
    public async Task<LoginResponseDto> ValidateUserAsync(string username, string password)
    {
        UserDetailsDto user = await _authRepository.ValidateUserAsync(username,password);
       
        if (user == null)
            return new  LoginResponseDto { Error="Invalid User" };
        string token = GenerateJwtToken(user);

        return new LoginResponseDto { Token = token };
    }

    // Generate JWT token
    public string GenerateJwtToken(UserDetailsDto user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Secret"])
        );

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
    {
        new Claim("Id", user.Id.ToString()),
        new Claim("UserName", user.UserName)
    };

        // Add roles (important fix)
        if (user.RoleDetails != null)
        {
            foreach (var role in user.RoleDetails)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
        }

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(jwtSettings["ExpiryMinutes"])
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Register user using MD5 hashing
    public async Task<bool> RegisterUserAsync(string username, string email, string password)
    {
        if (await _authRepository.CheckUserExistsAsync(username,email))
        {
            return false; // User exists
        }

        string hashedPassword = password.ToMd5Hash();

        var user = new UserDetailsDto
        {
            UserName = username,
            Email = email,
            Password = hashedPassword
        };

        return await _authRepository.AddAsync(user);
    }
}
