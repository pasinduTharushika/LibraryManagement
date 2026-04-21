using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.DTO
{
    public  class LoginResponseDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}
