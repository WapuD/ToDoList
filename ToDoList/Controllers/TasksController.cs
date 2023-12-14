using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ToDoListContext _context;

        public TasksController(ToDoListContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks tasks)
        {
            if (tasks.user.UserId == 0)
            {
                var user = await _context.Users.FindAsync(tasks.TaskUserId);
                tasks.user = user;
            }
            _context.Entry(tasks).State = EntityState.Modified;
            try{ await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

/*        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks tasks)
        {
            if (tasks.user.UserName.Length < 3 || tasks.user.UserName == null)
            {
                var user = await _context.Users.FindAsync(tasks.TaskUserId);
                tasks.user = user;
            }
            _context.Tasks.Add(tasks);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTasks", new { id = tasks.TaskId }, tasks);
        }*/

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(TaskDTO task)
        {
            var tasks = new Tasks
            {
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                TaskDuration = task.TaskDuration,
                TaskPriority = task.TaskPriority,
                TaskIsConfirm = task.TaskIsConfirm,
                TaskUserId = task.TaskUserId
            };
            tasks.user = await _context.Users.FindAsync(task.TaskUserId);
            await _context.Tasks.AddAsync(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, null, null);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}
