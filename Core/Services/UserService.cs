using Data.Repositories;
using Data.Entities;
using Core.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ReturnUserDTO?> GetByIdAsync(int id)
        {
            var temp = _userRepository.FindByCondition(x => x.Id == id, x => x.TodoLists);
            temp = temp.Include(x => x.TodoLists).ThenInclude(x => x.Tasks);
            var x = temp.First();
            return _mapper.Map<ReturnUserDTO>(x);
        }

        public async Task<IEnumerable<ReturnUserDTO>> GetAllAsync(GetUserDTO dto)
        {
            var temp = _userRepository.FindByCondition(x => true, x => x.TodoLists);
            temp = temp.Include(x => x.TodoLists).ThenInclude(x => x.Tasks);
            if(dto.Id != null)
                temp = temp.Where(x => x.Id == dto.Id);

            return _mapper.Map<List<ReturnUserDTO>>(await temp.ToListAsync());
        }

        public async Task<ReturnUserDTO> CreateAsync(GetUserDTO dto)
        {
            var temp = await _userRepository.FindByConditionAsync(x => x.UserName == dto.UserName);
            if (temp.Count() != 0)
                throw new Exception("The username has been used before");

            dto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
            User x = _mapper.Map<User>(dto);
            await _userRepository.CreateAsync(x);
            return _mapper.Map<ReturnUserDTO>(x);

        }

        public async Task<ReturnUserDTO> UpdateAsync(ChangePasswordUserDTO dto)
        {
            User temp = await _userRepository.GetByIdAsync(dto.Id);
            if (temp == null)
                throw new Exception("Id isn't in the System!");

            bool verify = BCrypt.Net.BCrypt.Verify(dto.OldPasswordHash, temp.PasswordHash);
            if (temp.UserName != dto.UserName || !verify)
                throw new Exception("Incorrect Username and Password");

            temp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPasswordHash);
            await _userRepository.UpdateAsync(temp);
            return _mapper.Map<ReturnUserDTO>(temp);
        }

        public async Task<ReturnUserDTO?> Authinticate(LoginUserDTO dto)
        {
            var temp = await _userRepository.FindByConditionAsync(x => x.UserName == dto.UserName);
            if (temp == null)
                return null;
            var x = temp.ToList().First();
            bool verify = BCrypt.Net.BCrypt.Verify(dto.PasswordHash, x.PasswordHash);
            return verify == true ? _mapper.Map<ReturnUserDTO>(x) : null;
        }

        public async Task DeleteAsync(int id)
        {
            var temp = await _userRepository.GetByIdAsync(id);
            if (temp == null)
                return;
            await _userRepository.DeleteAsync(temp);
            return;
        }

    }
}
