using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Models;
using ToDoList.Controllers;

namespace ToDoList.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
/*            using (var context = new ToDoListContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ToDoListContext>>()))
            {
                // Look for any movies.
                if (context.Users.Any() && context.Tasks.Any())
                {
                    return;   // DB has been seeded
                }
                context.Users.AddRange(
                    new Users
                    {
                        UserName = "qwe",
                        UserPasswordHash = "123"
                    },
                    new Users
                    {
                        UserName = "asd",
                        UserPasswordHash = "123"
                    },
                    new Users
                    {
                        UserName = "zxc",
                        UserPasswordHash = "123"
                    }
                );
                context.Tasks.AddRange(
                    new Tasks
                    {
                        TaskName = "Zero",
                        TaskDescription = "123",
                        TaskPriority = "Low",
                        TaskIsConfirm = false,
                        TaskUserId = 1,
                    },
                    new Tasks
                    {
                        TaskName = "One",
                        TaskDescription = "234",
                        TaskPriority = "Middle",
                        TaskIsConfirm = true,
                        TaskUserId = 2
                    }
                );
                context.SaveChanges();
            }*/
        }
    }
}
