using SurveyBasket.API.Contracts.Authentacations;

namespace SurveyBasket.API.Services
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterViewModel model , CancellationToken cancellationToken = default);
        Task<Result<AuthDto>> GetTokenForUserAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthDto>> GetRefreshTokenAsync( string token , string refeshToken , CancellationToken cancellationToken = default);
        Task<Result> RevokedOnRefreshTokenAsync( string token , string refeshToken , CancellationToken cancellationToken = default);
    }
}
