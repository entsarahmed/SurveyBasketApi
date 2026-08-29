using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]//  /api/polls
    [ApiController]
    public class PollsController(IPollService pollService) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_pollService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id <= 0)
                return NotFound("Id is not Found");

            var poll = _pollService.Get(id);

            //if(poll is null)
            //    return NotFound("poll is null");


            return poll is null ? NotFound("Poll is not Found") : Ok(poll);
        }

        [HttpPost("")]
        public IActionResult Add(Poll request)
        {
            var newPoll = _pollService.Add(request);
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Poll request)
        {
          var isUpdated =  _pollService.Update(id, request);

            if (!isUpdated)
                return NotFound();
            return NoContent();
        }



    }
}
