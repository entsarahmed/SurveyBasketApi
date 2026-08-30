namespace SurveyBasket.Api.Contracts.Response
{
    public class PollResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static implicit operator PollResponse(Poll poll)
        {
            return new()
            {
                Id = poll.Id,
                Title = poll.Title,
                Description = poll.Description,
            };
        }
    }
}
