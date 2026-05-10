using Core.Entities;
using LibraryManagement.Application.DTO;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories
{
    public  class AuthRepository : IAuthRepository
    {
        private readonly LibraryContext _context;

        public AuthRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<UserDetailsDto> ValidateUserAsync(string username ,string password)
        {
           
            //check valid user 
            var userDetailsDto = await _context.Users.AsNoTracking()
             .Where(u => u.Name == username)
             .Select(u => new UserDetailsDto
             {
                 Id = u.Id,
                 UserName = u.Name,
                 Email = u.Email,
                 Password = u.Password,
                 RoleDetails = u.UserRoles
                     .Select(ur => new UserRoleDetailsDto
                     {
                         RoleName = ur.Role.RoleName
                     })
                     .ToList()
             })
             .FirstOrDefaultAsync();

            if (userDetailsDto == null)
                return null;
              
            
                

            // Hash incoming password and compare
            string hashedInput = password.ToMd5Hash();

            if (!string.Equals(userDetailsDto.Password, hashedInput, StringComparison.OrdinalIgnoreCase))
                return null;



            return userDetailsDto;




        }

        public async Task<bool> CheckUserExistsAsync(string username, string email)
        {
            return await _context.Users.AsNoTracking().AnyAsync(u => u.Name == username || u.Email == email);
        }

        public async Task<bool> AddAsync(UserDetailsDto userdtl)
        {
            // map DTO → Entity
            string hashedPassword = userdtl.Password.ToMd5Hash();
            var user = new User
            {
                Name = userdtl.UserName,
                Email = userdtl.Email,
                Password = hashedPassword
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
            
        }

    }
}
