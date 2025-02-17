using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class RefreshTokenDTO
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
