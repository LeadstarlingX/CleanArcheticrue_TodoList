using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>()
                .HasOne(t => t.User)
                .WithMany(t => t.TodoLists)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.TodoList)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TodoListID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
