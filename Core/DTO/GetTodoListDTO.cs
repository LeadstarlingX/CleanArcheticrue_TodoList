﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class GetTodoListDTO
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public int? Id { get; set; }
    }
}
