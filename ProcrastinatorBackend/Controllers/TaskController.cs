using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcrastinatorBackend.DTO;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ProcrastinatorDbContext _dbContext;

        public TaskController(ProcrastinatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {

            try
            {
                var result = _dbContext.Tasks
                    .Include(t => t.User)
                    .Select(t => new
                    {
                        t.Taskid,
                        t.Task1,
                        t.Deadline,
                        t.Details,
                        t.Iscomplete,
                        User = new
                        {
                            t.User.Userid,
                            t.User.Firstname,
                        }
                    })
                    .OrderBy(t => t.Deadline)
                    .ToList();
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching tasks", details = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetMealsById(int id)
        {
            List<Models.Task> result = _dbContext.Tasks.Where(u => u.Userid == id).OrderBy(u => u.Deadline).ToList();
            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpPost]

        public IActionResult AddTask(TaskDTO newTask)
        {
            Models.Task t = new Models.Task
            {
                Userid = newTask.Userid,
                Task1 = newTask.Task1,
                Deadline = newTask.Deadline,
                Details = newTask.Details,
                Iscomplete = false,
                Created = DateOnly.FromDateTime(DateTime.Now),

            };
            _dbContext.Tasks.Add(t);
            _dbContext.SaveChanges();
            return Created($"/Api/Task/{t.Userid}", t);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateUser(int id, TaskDTO updateTask)
        {
            Models.Task t = _dbContext.Tasks.Find(id);
            if (t == null) { return NotFound(); }

            t.Task1 = updateTask.Task1;
            t.Deadline = updateTask.Deadline;
            t.Details = updateTask.Details;
            t.Iscomplete = updateTask.Iscomplete;

            _dbContext.Tasks.Update(t);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteTask(int id)
        {
            Models.Task t = _dbContext.Tasks.Find(id);
            if (t == null) { return NotFound(); }
            _dbContext.Tasks.Remove(t);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
