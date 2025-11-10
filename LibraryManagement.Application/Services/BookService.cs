using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Book>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Book?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Book> AddAsync(Book book) => _repository.AddAsync(book);
        public Task<Book> UpdateAsync(Book book) => _repository.UpdateAsync(book);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
