using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.DTO
{
    public class CreateBorrowRecordDto
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
    }
}
