namespace SurveyBasket.API.Contracts.Authentacations
{
    public record AuthDto
        (
            string ID,
            string FirstName,
            string LastName,
            string Email,
            string Token,
            int ExpireIn ,
            string RefreshToken,
            DateTime RefreshTokenExpiration

        );
}
