using LibraryManagement.Application.Interfaces.Repositories;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly LibraryContext _context;

        public BorrowRecordRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowRecord>> GetAllAsync()
        {
            return await _context.BorrowRecords
                .Include(x => x.Book)
                .Include(x => x.Member)
                .ToListAsync();
        }

        public async Task<BorrowRecord?> GetByIdAsync(int id)
        {
            return await _context.BorrowRecords
                .Include(x => x.Book)
                .Include(x => x.Member)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BorrowRecord> AddAsync(BorrowRecord record)
        {
            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<BorrowRecord> UpdateAsync(BorrowRecord record)
        {
            _context.BorrowRecords.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null) return false;

            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
