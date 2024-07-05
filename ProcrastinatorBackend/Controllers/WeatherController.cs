using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWeather(string city)
        {
            WeatherModel result = WeatherDAL.GetWeather(city);
            return Ok(result);
        }
    }
}
