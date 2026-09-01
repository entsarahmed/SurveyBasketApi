using Mapster;
using MapsterMapper;
using SurveyBasket.Api.Contracts.Request;
using SurveyBasket.Api.Contracts.Response;

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
            var polls = _pollService.GetAll();
            var response = polls.Adapt<IEnumerable<Poll>>();

            return Ok(response);
        }

        //[HttpGet("{id:int:min(10)}")]
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (id <= 0)
                return NotFound("Id is not Found");

            var poll = _pollService.Get(id);

            if(poll is null)
              return NotFound("poll is null");
        
          var response = poll.Adapt<PollResponse>();

            return Ok(response);
        }

        [HttpPost("")]
        public IActionResult Add([FromBody]CreatePollRequest request)
        {
          //  if(!ModelState.IsValid)
          //  {
          //      return ValidationProblem(ModelState);
          //  }
            var newPoll = _pollService.Add(request.Adapt<Poll>());
            return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
         
        
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id,[FromBody] CreatePollRequest request)
        {
        var isUpdated =  _pollService.Update(id, request.Adapt<Poll>());
        
          if (!isUpdated)
              return NotFound();
            return NoContent();
          
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id) {
            var isDeleted = _pollService.Delete(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }

        [HttpPost("test")]
        public IActionResult Test([FromBody] Student request)
        {
                    return Ok("Value accepted");
        }

    }
}
