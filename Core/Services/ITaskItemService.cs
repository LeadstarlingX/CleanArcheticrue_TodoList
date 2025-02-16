using Core.DTO;

namespace Core.Services
{
    public interface ITaskItemService
    {
        Task<ReturnTaskItemDTO?> GetbyIdAsync(int id);
        Task<IEnumerable<ReturnTaskItemDTO>> GetAllAsync(GetTaskItemDTO dto);
        Task<ReturnTaskItemDTO> CreateAsync(GetTaskItemDTO dto);
        Task<ReturnTaskItemDTO> UpdateAsync(GetTaskItemDTO dto);
        Task DeleteAsync(int id);
    }
}
