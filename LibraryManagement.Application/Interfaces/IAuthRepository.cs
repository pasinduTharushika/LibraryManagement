using Core.Entities;
using LibraryManagement.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDetailsDto> ValidateUserAsync(string username, string password);
        Task<bool> CheckUserExistsAsync(string username, string email);
        Task<bool> AddAsync(UserDetailsDto user);
    }
}
