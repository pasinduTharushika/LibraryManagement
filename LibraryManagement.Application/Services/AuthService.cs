using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    // Validate user using MD5 hashing
    public async Task<User?> ValidateUserAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null)
            return null;

        // Hash incoming password and compare
        string hashedInput = password.ToMd5Hash();

        if (!string.Equals(user.Password, hashedInput, StringComparison.OrdinalIgnoreCase))
            return null;

        return user;
    }

    // Generate JWT token
    public string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Register user using MD5 hashing
    public async Task<User?> RegisterUserAsync(string username, string email, string password)
    {
        if (await _userRepository.GetByUsernameAsync(username) != null ||
            await _userRepository.GetByEmailAsync(email) != null)
        {
            return null; // User exists
        }

        string hashedPassword = password.ToMd5Hash();

        var user = new User
        {
            Name = username,
            Email = email,
            Password = hashedPassword
        };

        return await _userRepository.AddAsync(user);
    }
}
