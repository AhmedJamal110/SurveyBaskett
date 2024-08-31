using SurveyBasket.API.Contracts.Authentacations;
using SurveyBasket.API.JwtService;
using System.Security.Cryptography;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly int _refreshTokenExpirationDays = 14;

        public AuthService(UserManager<ApplicationUser> userManager , 
            SignInManager<ApplicationUser> signInManager , IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<Result<AuthDto>> GetTokenForUserAsync(string email, string password, CancellationToken cancellationToken = default)
        {
           // var user = await _userManager.FindByEmailAsync(email);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user is null)
                    return Result.Failure<AuthDto>(UserErrors.InvalidCredential);
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (!result.Succeeded)
                    return Result.Failure<AuthDto>(UserErrors.InvalidCredential);

             var (token , expireIn)  = await _jwtProvider.CreateTokenAsync(user);
            var refreshToken = GeneraterefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration,

            });
            await _userManager.UpdateAsync(user);

            var response = new AuthDto
                (user.Id , user.FirstName, user.LastName, user.Email, token, expireIn, refreshToken , refreshTokenExpiration);
            return Result.Success(response);

        }

        public async Task<Result> RegisterAsync(RegisterViewModel model, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.FirstName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.First();
                return Result.Failure(new Error(errors.Code, errors.Description));
            }

            return Result.Success();
        }
 
        private static string GeneraterefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public  async Task<Result<AuthDto>> GetRefreshTokenAsync(string token, string refeshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.VaildateToken(token);
            if (userId is null)
                return Result.Failure<AuthDto>(UserErrors.InvalidCodeOrToken);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure<AuthDto>(UserErrors.InvalidCodeOrToken);


            var userRefreshToken =  user.RefreshTokens.SingleOrDefault(x => x.Token == refeshToken && x.IsActive);
            if (userRefreshToken is null)
                return Result.Failure<AuthDto>(UserErrors.InvalidCodeOrToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

           var (newToken , newExpireIn) = await  _jwtProvider.CreateTokenAsync(user);

            var newRefreshToken = GeneraterefreshToken();
            var newRefreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = newRefreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);
           
            var response = new AuthDto
                (user.Id, user.FirstName, user.LastName, user.Email, newToken, newExpireIn, newRefreshToken, newRefreshTokenExpiration);

                return Result.Success(response);

        }

        public async Task<Result> RevokedOnRefreshTokenAsync(string token, string refeshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.VaildateToken(token);
                if (userId is null)
                    return Result.Failure(UserErrors.InvalidCodeOrToken);

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure(UserErrors.InvalidCodeOrToken);

           var userRefreshToken =  user.RefreshTokens.SingleOrDefault(x => x.Token == refeshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(UserErrors.InvalidCodeOrToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            
            return Result.Success();
        }
    }
}
