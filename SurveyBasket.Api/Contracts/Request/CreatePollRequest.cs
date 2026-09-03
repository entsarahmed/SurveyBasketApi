namespace SurveyBasket.Api.Contracts.Request
{
    public record CreatePollRequest(
      string Title,
      string Summary,
      bool IsPublished,
      DateOnly StartsAt,
      DateOnly EndsAt
    );
}
