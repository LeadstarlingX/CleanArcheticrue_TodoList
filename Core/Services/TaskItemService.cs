using Core.DTO;
using Data.Repositories;
using Data.Entities;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class TaskItemService : ITaskItemService
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
        
        public async Task<IEnumerable<ReturnTaskItemDTO>> GetAllAsync(GetTaskItemDTO dto)
        {
            var temp = _taskItemRepository.GetAllQuery();
            if (dto.Id != null)
                temp = temp.Where(x => x.Id == dto.Id);
            if (dto.Name != null)
                temp = temp.Where(x => x.Name == dto.Name);
            if(dto.IsCompleted != null)
                temp = temp.Where(x => x.IsCompleted == dto.IsCompleted);
            if (dto.TodoListId != null)
                temp = temp.Where(x => x.TodoListID == dto.TodoListId);

            return _mapper.Map<List<ReturnTaskItemDTO>>(await temp.ToListAsync());
        }

        public async Task<ReturnTaskItemDTO> CreateAsync(GetTaskItemDTO dto)
        {
            if (dto.Name == null || dto.TodoListId == null)
                throw new Exception("Name and TodoListId can't be null");
            TaskItem x = _mapper.Map<TaskItem>(dto);
            await _taskItemRepository.CreateAsync(x);
            return _mapper.Map<ReturnTaskItemDTO>(x);
        }
        
        public async Task<ReturnTaskItemDTO> UpdateAsync(GetTaskItemDTO dto)
        {
            if (dto.Name == null || dto.TodoListId == null)
                throw new Exception("Name and TodoListId can't be null!");
            TaskItem x = _mapper.Map<TaskItem>(dto);
            await _taskItemRepository.UpdateAsync(x);
            return _mapper.Map<ReturnTaskItemDTO>(x);
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
