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
                IsComplete = newTask.IsComplete,
                Created = newTask.Created,

            };
            dbContext.Tasks.Add(t);
            dbContext.SaveChanges();
            return Created($"/Api/Task/{t.UserId}", t);
        }
    }
}
