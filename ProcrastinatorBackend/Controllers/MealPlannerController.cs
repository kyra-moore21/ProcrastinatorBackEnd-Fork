using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcrastinatorBackend.DTO;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlannerController : ControllerBase
    {
        private readonly ProcrastinatorDbContext _dbContext;

        public MealPlannerController(ProcrastinatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetMeals()
        {
            try
            {
                var result = _dbContext.MealPlanners
                    .Include(m => m.User)
                    .Select(m => new
                    {
                        m.Mealid,
                        m.Title,
                        m.Url,
                        m.Like,
                        m.Iscompleted,
                        User = new
                        {
                            m.User.Userid,
                            m.User.Firstname,
                        }
                    })
                    .ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while fetching meal planners", details = ex.Message });
            }    
        }
        [HttpGet("{id}")]

        public IActionResult GetMealsById(int id)
        {
            List<Mealplanner> result = _dbContext.MealPlanners.Where(u => u.Userid == id).ToList();
            if (result == null) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMeal(MealPlannerDTO newMeal)
        {
            Mealplanner m = new Mealplanner
            {
                Userid = newMeal.UserId,
                Title = newMeal.Title,
                Url = newMeal.Url,
                Like = false,
                Iscompleted = false,

            };
            _dbContext.MealPlanners.Add(m);
            _dbContext.SaveChanges();
            return Created($"/api/MealPlanner/{m.Mealid}", m);
        }
        [HttpPut("{id}")]

        public IActionResult UpdateMeal(MealPlannerDTO meal, int id, bool Like, bool IsCompleted)
        {

           Mealplanner m = _dbContext.MealPlanners.Find(id);
            if (m == null) { return NotFound(); }
            m.Userid = meal.UserId;
            m.Title = meal.Title;
            m.Url = meal.Url;
            m.Like = meal.Like;
            m.Iscompleted = meal.IsCompleted;

            _dbContext.MealPlanners.Update(m);
            _dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]

        public IActionResult deleteMeal(int id)
        {
            Mealplanner m = _dbContext.MealPlanners.Find(id);
            if (m == null) { return NotFound(); }
            _dbContext.MealPlanners.Remove(m);
            _dbContext.SaveChanges();
            return NoContent();
        }






    }
}