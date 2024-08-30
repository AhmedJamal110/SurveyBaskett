namespace SurveyBasket.API.Contracts.Authentacations
{
    public record RegisterViewModel
        (
           string FirstName,
           string LastName,
           string Email,
           string Password
        );
}
