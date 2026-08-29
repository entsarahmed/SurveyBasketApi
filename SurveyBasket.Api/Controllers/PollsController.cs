
namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]//  /api/polls
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly List<Poll> _polls = [
            new Poll{ 
            Id =1,
            Title="Poll 1",
            Description="Description 1",
            }
            ];

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_polls);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return NotFound("Id is not Found");

            var poll = _polls.FirstOrDefault(p => p.Id == id);

            //if(poll is null)
            //    return NotFound("poll is null");


            return poll is null ? NotFound("Poll is not Found") : Ok(poll);
        }
       
    }
}
