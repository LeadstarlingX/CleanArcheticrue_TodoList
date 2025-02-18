using Data.Repositories;
using Data.Entities;
using Core.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public ReturnUserDTO? GetById()
        {
            int id = _authenticationService.RetrieveUserID();
            var temp = _userRepository.FindByCondition(x => x.Id == id, x => x.TodoLists);
            temp = temp.Include(x => x.TodoLists).ThenInclude(x => x.Tasks);
            var x = temp.First();
            return _mapper.Map<ReturnUserDTO>(x);
        }

        public async Task<IEnumerable<ReturnUserDTO>> GetAllAsync(GetUserDTO dto)
        {
            var temp = _userRepository.FindByCondition(x => true, x => x.TodoLists);
            temp = temp.Include(x => x.TodoLists).ThenInclude(x => x.Tasks);
            if(dto.UserName != null)
                temp = temp.Where(x => x.UserName == dto.UserName);

            return _mapper.Map<List<ReturnUserDTO>>(await temp.ToListAsync());
        }

        public async Task<ReturnUserDTO> CreateAsync(GetUserDTO dto)
        {
            var temp = await _userRepository.FindByConditionAsync(x => x.UserName == dto.UserName);
            if (temp.Any())
                throw new Exception("The username has been used before");

            dto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
            User x = _mapper.Map<User>(dto);
            x.RefreshToken = await _authenticationService.RefreshToken();
            await _userRepository.CreateAsync(x);
            return _mapper.Map<ReturnUserDTO>(x);

        }

        public async Task<ReturnUserDTO> UpdateAsync(ChangePasswordUserDTO dto)
        {
            int id = _authenticationService.RetrieveUserID();
            User? temp = await _userRepository.GetByIdAsync(id);
            if (temp == null)
                throw new Exception("Id isn't in the System!");

            bool verify = BCrypt.Net.BCrypt.Verify(dto.OldPasswordHash, temp.PasswordHash);
            if (temp.UserName != dto.UserName || !verify)
                throw new Exception("Incorrect Username and Password");

            temp.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPasswordHash);
            await _userRepository.UpdateAsync(temp);
            return _mapper.Map<ReturnUserDTO>(temp);
        }

        public async Task DeleteAsync()
        {
            int id = _authenticationService.RetrieveUserID();
            var temp = await _userRepository.GetByIdAsync(id);
            if (temp == null)
                return;
            await _userRepository.DeleteAsync(temp);
            return;
        }


    }
}
