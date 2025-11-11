
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        Task<User?> ValidateUserAsync(string username, string password);
        Task<User?> RegisterUserAsync(string username, string email, string password);
    }
}
