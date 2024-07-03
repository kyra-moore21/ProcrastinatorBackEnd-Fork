using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcrastinatorBackend.Models;

namespace ProcrastinatorBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetQuote()
        {
            QuoteModel[] result = QuoteDAL.GetQuote();
            return Ok(result);
        }
    }
}
