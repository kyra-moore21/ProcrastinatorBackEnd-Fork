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
        ProcrastinatorDbContext dbContext = new ProcrastinatorDbContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> result = dbContext.Users.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            User result = dbContext.Users.Find(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUser(UserDTO newUser)
        {
            if (dbContext.Users.Any(u => u.Email.ToLower() == newUser.Email.ToLower()))
            {
                return Ok(dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == newUser.Email.ToLower()));
            } else
            {
            User u = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhotoUrl = newUser.PhotoUrl,
                Display = newUser.Display,
            };

            dbContext.Users.Add(u);
            dbContext.SaveChanges();
            return Created($"/Api/User/{u.UserId}", u);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDTO updatedUser)
        {
            User u = dbContext.Users.Find(id);
            if(u == null) { return NotFound(); }
            u.FirstName = updatedUser.FirstName;
            u.LastName = updatedUser.LastName;
            u.Email = updatedUser.Email;
            u.PhotoUrl = updatedUser.PhotoUrl;
            u.Display = updatedUser.Display;
            dbContext.Users.Update(u);
            dbContext.SaveChanges();
            return NoContent();
            
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteUser(int id)
        {

            User u = dbContext.Users.Include(u => u.Tasks).Include(u => u.MealPlanners).FirstOrDefault(u => u.UserId == id);
            if (u == null) { return NotFound(); } 
            dbContext.MealPlanners.RemoveRange(u.MealPlanners);
            dbContext.Tasks.RemoveRange(u.Tasks);
            dbContext.SaveChanges();

            dbContext.Users.Remove(u);
            dbContext.SaveChanges();
            return NoContent();
         }
    }
}
