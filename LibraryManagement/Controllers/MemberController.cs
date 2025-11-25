using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _memberService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateMemberDto dto)
        {
            var newMember = await _memberService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemberDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var updatedMember = await _memberService.UpdateAsync(dto);
            return Ok(updatedMember);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _memberService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
