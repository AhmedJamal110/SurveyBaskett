using SurveyBasket.API.Contracts.Authentacations;

namespace SurveyBasket.API.Services
{
    public interface IAuthService
    {
        Task<Result<AuthDto>> GetTokenForUserAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result> RegisterAsync(RegisterViewModel model , CancellationToken cancellationToken = default);
    }
}
