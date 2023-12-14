using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;
namespace ToDoList.Models
{
    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TaskId")]
        public int TaskId { get; set; }
        [Column("TaskName")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Поле не должно содержать спецсимволы")]
        public string TaskName { get; set; } = string.Empty;
        [Column("TaskDescription")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 1000 символов")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Поле не должно содержать спецсимволы")]
        public string TaskDescription { get; set; } = string.Empty;
        [Column("TaskDuration")]
        public DateOnly TaskDuration { get; set; }
        [Column("TaskPriority")]
        public string TaskPriority { get; set; } = string.Empty;
        [Column("TaskIsConfirm")]
        public bool TaskIsConfirm { get; set; }
        [Column("TaskUserId")]
        public int TaskUserId { get; set; }
        public Users user { get; set; } = new Users();
    }
}