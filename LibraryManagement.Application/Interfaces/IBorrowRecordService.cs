
using LibraryManagement.Domain.DTO;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBorrowRecordService
    {
        Task<IEnumerable<BorrowRecord>> GetAllAsync();
        Task<BorrowRecord?> GetByIdAsync(int id);
        Task<BorrowRecord> BorrowAsync(CreateBorrowRecordDto dto);
        Task<BorrowRecord> ReturnAsync(ReturnBookDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
