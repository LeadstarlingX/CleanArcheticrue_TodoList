using Data.Entities;

namespace Data.Respositories
{
    public interface IRepositoryManager
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<TodoList> TodoListRepository { get; }
        IGenericRepository<TaskItem> TaskItemRepository { get; }
    }
}
