using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProcrastinatorBackend.DTO;
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
            MealPlanner result = dbContext.MealPlanners.Include(u => u.User).FirstOrDefault(u => u.MealId == id);
            if (result == null) { return NotFound(); }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddMeal(MealPlannerDTO newMeal)
        {
            MealPlanner m = new MealPlanner
            {
                UserId = newMeal.UserId,
                Title = newMeal.Title,
                Url = newMeal.Url,
                Like = false,
                IsCompleted = false,

            };
            dbContext.MealPlanners.Add(m);
            dbContext.SaveChanges();
            return Created($"/api/MealPlanner/{m.MealId}", m);
        }
        [HttpPut("{id}")]

        public IActionResult UpdateMeal(MealPlannerDTO meal, int id, bool Like, bool IsCompleted)
        {

           MealPlanner m = dbContext.MealPlanners.Find(id);
            if (m == null) { return NotFound(); }
            m.UserId = meal.UserId;
            m.Title = meal.Title;
            m.Url = meal.Url;
            m.Like = Like;
            m.IsCompleted = IsCompleted;

            dbContext.MealPlanners.Update(m);
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