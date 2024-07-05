using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlannerController : ControllerBase
    {
        ProcrastinatorDbContext dbContext = new ProcrastinatorDbContext();
        [HttpGet]
        public IActionResult GetMeals()
        {
            List<MealPlanner> result = dbContext.MealPlanners.Include(m => m.User).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]

        public IActionResult GetMealsById(int id)
        {
            MealPlanner result = dbContext.MealPlanners.Include(u => u.UserId).SingleOrDefault();
            if (result == null) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMeal([FromBody] MealPlanner newMeal)
        {
            newMeal.MealId = 0;
            dbContext.MealPlanners.Add(newMeal);
            dbContext.SaveChanges();
            return Created($"/api/MealPlanner/{newMeal.MealId}", newMeal);
        }
        [HttpPut("{id}")]

        public IActionResult UpdateMeal([FromBody] MealPlanner targetMeal, int id)
        {

            if (targetMeal.MealId != id) { return BadRequest(); }
            if (!dbContext.MealPlanners.Any(m => m.MealId == id)) { return NotFound(); }
            dbContext.MealPlanners.Update(targetMeal);
            dbContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete]

        public IActionResult deleteMeal(int id)
        {
            MealPlanner m = dbContext.MealPlanners.Find(id);
            if (m == null) { return NotFound(); }
            dbContext.MealPlanners.Remove(m);
            dbContext.SaveChanges();
            return NoContent();
        }






    }
}