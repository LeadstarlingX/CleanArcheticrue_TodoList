using Core.DTO;

namespace Core.Services
{
    public interface IUserService
    {
        Task<ReturnUserDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ReturnUserDTO>> GetAllAsync(GetUserDTO dto);
        Task<ReturnUserDTO> CreateAsync(GetUserDTO dto);
        Task<ReturnUserDTO> UpdateAsync(ChangePasswordUserDTO dto);
        Task DeleteAsync(int id);

        Task<ReturnUserDTO> Authinticate(LoginUserDTO dto);

    }
}
