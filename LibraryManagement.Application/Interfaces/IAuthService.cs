
using Core.Entities;
using LibraryManagement.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> ValidateUserAsync(string username, string password);
        Task<bool> RegisterUserAsync(string username, string email, string password);
    }
}
