
namespace SurveyBasket.API.Contracts.Authentacations
{
    public class RefreshTokenViewModelVaildators : AbstractValidator<RefreshTokenViewModel>
    {
        public RefreshTokenViewModelVaildators()
        {
            RuleFor(x => x.Token)
                .NotEmpty();


            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }

    }
}
