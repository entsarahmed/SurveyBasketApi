using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.Api.Contracts.Polls;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]//  /api/polls
    [ApiController]
    
    public class PollsController(IPollService pollService,IConfiguration configuration) : ControllerBase
    {
        private readonly IPollService _pollService = pollService;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var polls = await _pollService.GetAllAsync(cancellationToken);
            var response = polls.Adapt<IEnumerable<Poll>>();

            return Ok(response);
        }

        //[HttpGet("{id:int:min(10)}")]
   [HttpGet("{id}")]
   public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken)
   {
       if (id <= 0)
           return NotFound("Id is not Found");
   
       var poll = await _pollService.GetAsync(id, cancellationToken);
   
       if(poll is null)
         return NotFound("poll is null");
   
     var response = poll.Adapt<PollResponse>();
   
       return Ok(response);
   }
     
      [HttpPost("")]
      public async Task<IActionResult> Add([FromBody]PollRequest request, CancellationToken cancellationToken)
      {
          
     
          var newPoll = await _pollService.AddAsync(request.Adapt<Poll>(),cancellationToken);
          return CreatedAtAction(nameof(Get), new { id = newPoll.Id }, newPoll);
       
      
      }
     
   [HttpPut("{id}")]
   public async Task<IActionResult> Update([FromRoute]int id,[FromBody] PollRequest request, CancellationToken cancellationToken)
   {
   var isUpdated = await _pollService.UpdateAsync(id, request.Adapt<Poll>(), cancellationToken);
   
     if (!isUpdated)
         return NotFound();
       return NoContent();
     
   }
  
      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken) {
          var isDeleted = await _pollService.DeleteAsync(id, cancellationToken);
          if (!isDeleted)
              return NotFound();
          return NoContent();
      }

        [HttpPut("{id}/togglepublish")]
        public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
        {
            var isToggled = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

            if (!isToggled)
                return NotFound();
            return NoContent();
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var config = new
            {
                //MyKey = _configuration["MyKey"]
                //MyKey = _configuration.GetConnectionString("DefaultConnection")
               // MyKey = _configuration["ConnectionStrings:DefaultConnection"]
              MyKey = _configuration["Logging:LogLevel:Default"],
               Env = _configuration["ASPNETCORE_ENVIRONMENT"],
               OneDrive = _configuration["OneDrive"]
            };
            return Ok(config);
        }
    }
}
