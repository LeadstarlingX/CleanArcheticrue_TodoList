using Data.Entities;

namespace Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _db;
        IGenericRepository<User> _userRepository;
        IGenericRepository<TodoList> _todoRepository;
        IGenericRepository<TaskItem> _taskRepository;
        public RepositoryManager(AppDbContext db)
        {
            _db = db;
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new GenericRepository<User>(_db);
                return _userRepository;
            }
        }

        public IGenericRepository<TodoList> TodoListRepository
        {
            get
            {
                if (_todoRepository == null)
                    _todoRepository = new GenericRepository<TodoList>(_db);
                return _todoRepository;
            }

        }

        public IGenericRepository<TaskItem> TaskItemRepository
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new GenericRepository<TaskItem>(_db);
                return _taskRepository;
            }
        }
    
    }
}
