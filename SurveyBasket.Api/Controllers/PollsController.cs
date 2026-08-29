
namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]//  /api/polls
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly List<Poll> _polls = [];

        [HttpGet]
        //[HttpGet("getAll2")]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_polls);
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(_polls);
        }
    }
}
