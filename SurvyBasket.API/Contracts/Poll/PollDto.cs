namespace SurveyBasket.API.Contracts.Poll
{
    public record PollDto(
        int ID,
        string Title,
        string Summary,
        bool IsPublished,
        DateOnly StartsAt,
        DateOnly EndsAt
        );
   
}
