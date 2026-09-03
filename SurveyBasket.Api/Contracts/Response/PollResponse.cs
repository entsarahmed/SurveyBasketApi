namespace SurveyBasket.Api.Contracts.Response
{
    public record PollResponse(int Id,
        string Title,
        string Summary,
        string IsPublished,
        DateOnly StartsAt,
        DateOnly EndsAt

    );
}
