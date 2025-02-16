using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ReturnUserDTO
    {
        public int Id {  get; set; }
        public string UserName { get; set; }
        public List<ReturnTodoListDTO> TodoLists { get; set; }
    }
}
