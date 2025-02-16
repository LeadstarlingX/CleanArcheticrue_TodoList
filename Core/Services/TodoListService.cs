using Data.Repositories;
using Core.DTO;
using Data.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Core.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly IGenericRepository<TodoList> _todoListRepository;
        private readonly IMapper _mapper;


        public TodoListService (IGenericRepository<TodoList> todoListRepository, IMapper mapper)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<ReturnTodoListDTO?> GetByIdAsync(int id)
        {
            var temp = _todoListRepository.FindByCondition(x => x.Id == id, x => x.Tasks);

            var test = await temp.ToListAsync();
            var x = test.First();
            return _mapper.Map<ReturnTodoListDTO>(x);
        }

        public async Task<IEnumerable<ReturnTodoListDTO>> GetAllAsync(GetTodoListDTO dto)
        {
            var temp = _todoListRepository.FindByCondition(x => true, x=> x.Tasks);
            if (dto.Id != null)
                temp = temp.Where(x => x.Id == dto.Id);
            if(dto.Name != null)
                temp = temp.Where(x => x.Name == dto.Name);
            if (dto.UserId != null)
                temp = temp.Where(x => x.UserId == dto.UserId);

            var test = await temp.ToListAsync();
            return _mapper.Map<List<ReturnTodoListDTO>>(test);

        }

        public async Task<ReturnTodoListDTO> CreateAsync(GetTodoListDTO dto)
        {
            if(dto.Name == null || dto.UserId == null)
                throw new Exception("UserId and Name can't be Null!");
            try
            {
                TodoList x = _mapper.Map<TodoList>(dto);
                await _todoListRepository.CreateAsync(x);
                return _mapper.Map<ReturnTodoListDTO>(x);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReturnTodoListDTO> UpdateAsync(GetTodoListDTO dto)
        {
            if (dto.Name == null || dto.Id == null)
                throw new Exception("ID and Name can't be Null!");

            var temp = await _todoListRepository.GetByIdAsync((int)dto.Id);
            if (temp == null)
                throw new Exception("This Id isn't found in the System!");
            try
            {
                temp.Name = dto.Name;
                await _todoListRepository.UpdateAsync(temp);
                return _mapper.Map<ReturnTodoListDTO>(temp);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var temp = await _todoListRepository.GetByIdAsync(id);
            if (temp == null)
                return;
            await _todoListRepository.DeleteAsync(temp);
        }
    
    }
}
