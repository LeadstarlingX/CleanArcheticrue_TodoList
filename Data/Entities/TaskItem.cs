using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class TaskItem
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool IsCompleted { get; set; }

        [ForeignKey("TodoList")]
        public int TodoListID { get; set; }
        public TodoList? TodoList { get; set; }

    }
}
