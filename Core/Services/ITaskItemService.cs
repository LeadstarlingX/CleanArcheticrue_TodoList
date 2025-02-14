using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Respositories;
using Data.Entities;
using Core.DTO;

namespace Core.Services
{
    internal interface ITaskItemService
    {
        Task<ReturnTaskItemDTO?> GetbyIdAsync(int id);
        Task<IEnumerable<ReturnTaskItemDTO?>> GetAllAsync();
        Task<ReturnTaskItemDTO> CreateAsync(GetTaskItemDTO dto);
        Task<ReturnTaskItemDTO> UpdateAsync(GetTaskItemDTO dto);
        Task DeleteAsync(int id);
    }
}
