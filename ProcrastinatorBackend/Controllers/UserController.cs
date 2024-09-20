using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcrastinatorBackend.DTO;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProcrastinatorDbContext _dbContext;

        public UserController(ProcrastinatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> result = _dbContext.Users.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            User result = _dbContext.Users.Find(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUser(UserDTO newUser)
        {
            if (_dbContext.Users.Any(u => u.Email.ToLower() == newUser.Email.ToLower()))
            {
                return Ok(_dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == newUser.Email.ToLower()));
            } else
            {
            User u = new User
            {
                Firstname = newUser.FirstName,
                Lastname = newUser.LastName,
                Email = newUser.Email,
                Photourl = newUser.PhotoUrl,
                Display = newUser.Display,
            };

            _dbContext.Users.Add(u);
            _dbContext.SaveChanges();
            return Created($"/Api/User/{u.Userid}", u);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDTO updatedUser)
        {
            User u = _dbContext.Users.Find(id);
            if(u == null) { return NotFound(); }
            u.Firstname = updatedUser.FirstName;
            u.Lastname = updatedUser.LastName;
            u.Email = updatedUser.Email;
            u.Photourl = updatedUser.PhotoUrl;
            u.Display = updatedUser.Display;
            _dbContext.Users.Update(u);
            _dbContext.SaveChanges();
            return NoContent();
            
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteUser(int id)
        {

            User u = _dbContext.Users.Include(u => u.Tasks).Include(u => u.Mealplanners).FirstOrDefault(u => u.Userid == id);
            if (u == null) { return NotFound(); } 
            _dbContext.MealPlanners.RemoveRange(u.Mealplanners);
            _dbContext.Tasks.RemoveRange(u.Tasks);
            _dbContext.SaveChanges();

            _dbContext.Users.Remove(u);
            _dbContext.SaveChanges();
            return NoContent();
         }
    }
}
