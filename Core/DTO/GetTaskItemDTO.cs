using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    internal class GetTaskItemDTO
    {
        public string? Name { get; set; }
        public bool? IsCompleted { get; set; }
        public int? TodoListId { get; set; }
        public int? Id { get; set; }
    }
}
