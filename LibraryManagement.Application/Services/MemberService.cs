
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.DTO;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public async Task<Member> AddAsync(CreateMemberDto dto)
        {
            var member = new Member
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                JoinedDate = DateTime.UtcNow,
                IsActive = true
            };

            return await _memberRepository.AddAsync(member);
        }

        public async Task<Member> UpdateAsync(UpdateMemberDto dto)
        {
            var existing = await _memberRepository.GetByIdAsync(dto.Id);
            if (existing == null) throw new Exception("Member not found");

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;

            return await _memberRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _memberRepository.DeleteAsync(id);
        }
    }
}
