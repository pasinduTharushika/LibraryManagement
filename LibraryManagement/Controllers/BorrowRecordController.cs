using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BorrowRecordController : ControllerBase
    {
        private readonly IBorrowRecordService _borrowService;

        public BorrowRecordController(IBorrowRecordService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _borrowService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _borrowService.GetByIdAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow(CreateBorrowRecordDto dto)
        {
            var newRecord = await _borrowService.BorrowAsync(dto);
            return Ok(newRecord);
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return(ReturnBookDto dto)
        {
            var updatedRecord = await _borrowService.ReturnAsync(dto);
            return Ok(updatedRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _borrowService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
