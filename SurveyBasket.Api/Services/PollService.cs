
using Microsoft.AspNetCore.Http.HttpResults;
using SurveyBasket.Api.Controllers;

namespace SurveyBasket.Api.Services
{
    public class PollService : IPollService
    {
        private readonly List<Poll> _polls = [
            new Poll{
                Id = 1,
                Title = "Poll 1",
                Description = "Description 1",
            }
            ];
        public IEnumerable<Poll> GetAll() => _polls;
        

        public Poll? Get(int id)
        =>
            _polls.FirstOrDefault(p => p.Id == id);

       

      
    }
}
