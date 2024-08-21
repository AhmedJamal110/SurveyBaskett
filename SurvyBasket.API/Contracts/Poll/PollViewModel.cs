namespace SurveyBasket.API.Contracts.Poll
{
    public record PollViewModel(
        string Title,
        string Summary,
        DateOnly StartsAt,
        DateOnly EndsAt
        );
    
}
