namespace SurveyBasket.Api.Contracts.Polls
{
    public record PollResponse(int Id,
        string Title,
        string Summary,
        string IsPublished,
        DateOnly StartsAt,
        DateOnly EndsAt

    );
}
