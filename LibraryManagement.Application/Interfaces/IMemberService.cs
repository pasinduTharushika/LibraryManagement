using LibraryManagement.Domain.DTO;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int id);
        Task<Member> AddAsync(CreateMemberDto dto);
        Task<Member> UpdateAsync(UpdateMemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
