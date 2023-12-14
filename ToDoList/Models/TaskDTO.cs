using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskDTO
    {
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Поле не должно содержать спецсимволы")]
        public string TaskName { get; set; }
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Поле не должно содержать спецсимволы")]
        public string TaskDescription { get; set; }
        public DateOnly TaskDuration { get; set; }
        public string TaskPriority { get; set; }
        public bool TaskIsConfirm { get; set; }
        public int TaskUserId { get; set; }
    }
}
