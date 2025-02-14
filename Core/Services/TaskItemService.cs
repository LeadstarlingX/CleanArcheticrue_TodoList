using Core.DTO;
using Data.Respositories;
using Data.Entities;
using AutoMapper;

namespace Core.Services
{
    internal class TaskItemService : ITaskItemService
    {
        private readonly IGenericRepository<TaskItem> _taskItemRepository;
        private readonly IMapper _mapper;

        
        public TaskItemService(IGenericRepository<TaskItem> taskItemRepository, IMapper mapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = mapper;
        }

        public async Task<ReturnTaskItemDTO?> GetbyIdAsync(int id)
        {
            var temp = await _taskItemRepository.GetByIdAsync(id);
            if (temp == null)
                return null;
            ReturnTaskItemDTO result = _mapper.Map<ReturnTaskItemDTO>(temp);
            return result;
        }
        
        public async Task<IEnumerable<ReturnTaskItemDTO?>> GetAllAsync()
        {
            var temp = await _taskItemRepository.GetAllAsync();

            IEnumerable<ReturnTaskItemDTO> result = [];
            List<ReturnTaskItemDTO> local = [];
            foreach (var item in temp)
            {
                ReturnTaskItemDTO x = _mapper.Map<ReturnTaskItemDTO>(item);
                local.Add(x);
            }
            result = local;
            return result;
        }
        
        public async Task<ReturnTaskItemDTO> CreateAsync(GetTaskItemDTO dto)
        {
            if (dto.Name == null || dto.TodoListId == null)
                throw new Exception("Name and TodoListId can't be null");
            TaskItem x = _mapper.Map<TaskItem>(dto);
            await _taskItemRepository.CreateAsync(x);
            ReturnTaskItemDTO result = _mapper.Map<ReturnTaskItemDTO>(x);
            return result;
        }
        
        public async Task<ReturnTaskItemDTO> UpdateAsync(GetTaskItemDTO dto)
        {
            if (dto.Name == null || dto.TodoListId == null)
                throw new Exception("Name and TodoListId can't be null!");
            TaskItem x = _mapper.Map<TaskItem>(dto);
            await _taskItemRepository.UpdateAsync(x);
            ReturnTaskItemDTO result = _mapper.Map<ReturnTaskItemDTO>(x);
            return result;
        }
        
        public async Task DeleteAsync(int id)
        {
            var x = await _taskItemRepository.GetByIdAsync(id);
            if (x == null)
                return;

            await _taskItemRepository.DeleteAsync(x);
            return;
        }
    }
}
