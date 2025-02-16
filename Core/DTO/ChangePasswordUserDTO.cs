using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ChangePasswordUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OldPasswordHash { get; set; }
        public string NewPasswordHash { get; set; }
    }
}
