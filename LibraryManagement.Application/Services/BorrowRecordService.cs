using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.DTO;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services
{
    public class BorrowRecordService : IBorrowRecordService
    {
        private readonly IBorrowRecordRepository _borrowRepository;

        public BorrowRecordService(IBorrowRecordRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public async Task<IEnumerable<BorrowRecord>> GetAllAsync()
        {
            return await _borrowRepository.GetAllAsync();
        }

        public async Task<BorrowRecord?> GetByIdAsync(int id)
        {
            return await _borrowRepository.GetByIdAsync(id);
        }

        public async Task<BorrowRecord> BorrowAsync(CreateBorrowRecordDto dto)
        {
            var record = new BorrowRecord
            {
                MemberId = dto.MemberId,
                BookId = dto.BookId,
                BorrowDate = DateTime.UtcNow,
                IsReturned = false
            };

            return await _borrowRepository.AddAsync(record);
        }

        public async Task<BorrowRecord> ReturnAsync(ReturnBookDto dto)
        {
            var record = await _borrowRepository.GetByIdAsync(dto.BorrowRecordId);
            if (record == null) throw new Exception("Record not found");

            record.IsReturned = true;
            record.ReturnDate = DateTime.UtcNow;

            return await _borrowRepository.UpdateAsync(record);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _borrowRepository.DeleteAsync(id);
        }
    }
}
