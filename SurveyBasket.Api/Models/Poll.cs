namespace SurveyBasket.Api.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Test test { get; set; } = default!;
    }

    public class Test {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
