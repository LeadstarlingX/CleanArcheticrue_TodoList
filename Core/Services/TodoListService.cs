using Data.Respositories;
using Core.DTO;
using Data.Entities;
using AutoMapper;

namespace Core.Services
{
    internal class TodoListService : ITodoListService
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
            var temp = await _todoListRepository.GetByIdAsync(id);
            if (temp == null)
                return null;
            
            ReturnTodoListDTO result = _mapper.Map<ReturnTodoListDTO>(temp);
            return result;
        }

        public async Task<IEnumerable<ReturnTodoListDTO?>> GetAllAsync()
        {
            var temp = await _todoListRepository.GetAllAsync();

            IEnumerable<ReturnTodoListDTO> result;
            List<ReturnTodoListDTO> local = [];
            foreach (var todo in temp)
            {
                ReturnTodoListDTO x = _mapper.Map<ReturnTodoListDTO>(todo);
                local.Add(x);
            }
            result = local;
            return result;
        }

        public async Task<ReturnTodoListDTO> CreateAsync(GetTodoListDTO dto)
        {
            if(dto.Name == null || dto.UserId == null)
                throw new Exception("UserId and Name can't be Null!");
            
            TodoList x = _mapper.Map<TodoList>(dto);
            await _todoListRepository.CreateAsync(x);
            ReturnTodoListDTO result = _mapper.Map<ReturnTodoListDTO>(x);
            return result;
        }

        public async Task<ReturnTodoListDTO> UpdateAsync(GetTodoListDTO dto)
        {
            if (dto.Name == null || dto.UserId == null)
                throw new Exception("UserId and Name can't be Null!");

            var temp = await _todoListRepository.FindByConditionAsync(x => x.Id == dto.Id);
            if (temp == null)
                throw new Exception("This Id isn't found in the System!");
            
            TodoList x = _mapper.Map<TodoList>(dto);
            await _todoListRepository.UpdateAsync(x);
            ReturnTodoListDTO result = _mapper.Map<ReturnTodoListDTO>(x);
            return result;
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
