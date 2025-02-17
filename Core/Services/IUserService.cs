using Core.DTO;

namespace Core.Services
{
    public interface IUserService
    {
        ReturnUserDTO? GetById();
        Task<IEnumerable<ReturnUserDTO>> GetAllAsync(GetUserDTO dto);
        Task<ReturnUserDTO> CreateAsync(GetUserDTO dto);
        Task<ReturnUserDTO> UpdateAsync(ChangePasswordUserDTO dto);
        Task DeleteAsync();

    }
}
