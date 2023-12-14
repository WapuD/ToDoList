using Refit;
using ToDoList.Models;

namespace ToDoListWeb.Models
{
    public interface InterfaceClient
    {
        [Get("/users")]
        Task<IEnumerable<Users>> GetAllUsers();
        [Get("/tasks")]
        Task<IEnumerable<Tasks>> GetAllTasks();

        [Get("/users/{id}")]
        Task<Users> GetUser(int id);
        [Get("/tasks/{id}")]
        Task<Tasks> GetTask(int id);

        [Post("/users")]
        Task<Users> CreateUser([Body] Users user);
        [Post("/tasks")]
        Task CreateTask([Body] TaskDTO task);

        [Put("/users/{id}")]
        Task RedactUser(int id, [Body] Users user);
        [Put("/tasks/{id}")]
        Task RedactTask(int id, [Body] Tasks task);

        [Delete("/users/{id}")]
        Task DeleteUser(int id);
        [Delete("/tasks/{id}")]
        Task DeleteTask(int id);

        [Post("/users/authorization")]
        Task<string> Authorize([Body] UserDTO user);
    }
}
