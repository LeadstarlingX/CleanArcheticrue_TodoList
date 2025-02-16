﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ReturnTodoListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ReturnTaskItemDTO> Tasks { get; set; }
    }
}
