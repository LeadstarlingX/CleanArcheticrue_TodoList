using Core.DTO;

namespace Core.Services
{
    public interface ITodoListService
    {
        Task<ReturnTodoListDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ReturnTodoListDTO>> GetAllAsync(GetTodoListDTO dto);
        Task<ReturnTodoListDTO> CreateAsync(GetTodoListDTO dto);
        Task<ReturnTodoListDTO> UpdateAsync(GetTodoListDTO dto);
        Task DeleteAsync(int id);

    }
}
