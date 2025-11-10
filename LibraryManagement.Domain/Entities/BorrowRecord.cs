using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class BorrowRecord
    {
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public int MemberId { get; private set; }
        public DateTime BorrowDate { get; private set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; private set; }

        public void ReturnBook() => ReturnDate = DateTime.UtcNow;
    }
}
