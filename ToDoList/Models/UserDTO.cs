using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class UserDTO
    {
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Поле не должно содержать спецсимволы")]
        public string UserName { get; set; }
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Поле не должно содержать спецсимволы")]
        public string UserPassword { get; set; }
    }
}
