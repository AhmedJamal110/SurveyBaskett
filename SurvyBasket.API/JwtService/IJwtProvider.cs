namespace SurveyBasket.API.JwtService
{
    public interface IJwtProvider
    {
        Task<(string token, int expireIn)> CreateTokenAsync(ApplicationUser user);
        string? VaildateToken(string token);
    }
}
