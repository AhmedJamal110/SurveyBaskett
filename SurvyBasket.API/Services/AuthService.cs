using SurveyBasket.API.Contracts.Authentacations;
using SurveyBasket.API.JwtService;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;

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

            var response = new AuthDto(user.Id , user.FirstName, user.LastName, user.Email, token, expireIn);
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
    }
}
