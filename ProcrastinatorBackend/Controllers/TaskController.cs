using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcrastinatorBackend.DTO;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ProcrastinatorDbContext dbContext = new ProcrastinatorDbContext();

        [HttpGet]
        public IActionResult GetTasks()
        {

            List<Models.Task> result = dbContext.Tasks.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Models.Task result = dbContext.Tasks.Find(id);
            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpPost]

        public IActionResult AddTask(TaskDTO newTask)
        {
            Models.Task t = new Models.Task
            {
                UserId = newTask.UserId,
                Task1 = newTask.Task,
                Deadline = newTask.Deadline,
                Details = newTask.Details,
                IsComplete = false,
                Created = DateOnly.FromDateTime(DateTime.Now),

            };
            dbContext.Tasks.Add(t);
            dbContext.SaveChanges();
            return Created($"/Api/Task/{t.UserId}", t);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateUser(int id, TaskDTO updateTask)
        {
            Models.Task t = dbContext.Tasks.Find(id);
            if (t == null) { return NotFound(); }

            t.Task1 = updateTask.Task;
            t.Deadline = updateTask.Deadline;
            t.Details = updateTask.Details;
            t.IsComplete = updateTask.IsComplete;

            dbContext.Tasks.Update(t);
            dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteTask(int id)
        {
            Models.Task t = dbContext.Tasks.Find(id);
            if (t == null) { return NotFound(); }
            dbContext.Tasks.Remove(t);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
