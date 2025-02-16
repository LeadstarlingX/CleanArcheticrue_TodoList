using Data.Entities;

namespace Data.Repositories
{
    public interface IRepositoryManager
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<TodoList> TodoListRepository { get; }
        IGenericRepository<TaskItem> TaskItemRepository { get; }
    }
}
