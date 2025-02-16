using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class GetUserDTO
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
    }
}
