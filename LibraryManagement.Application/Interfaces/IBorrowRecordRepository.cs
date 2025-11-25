using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces.Repositories
{
    public interface IBorrowRecordRepository
    {
        Task<IEnumerable<BorrowRecord>> GetAllAsync();
        Task<BorrowRecord?> GetByIdAsync(int id);
        Task<BorrowRecord> AddAsync(BorrowRecord record);
        Task<BorrowRecord> UpdateAsync(BorrowRecord record);
        Task<bool> DeleteAsync(int id);
    }
}
