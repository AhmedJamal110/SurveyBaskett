namespace SurveyBasket.API.Contracts.Authentacations
{
    public class AuthViewModelValidators : AbstractValidator<AuthViewModel>
    {
        public AuthViewModelValidators()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();


            RuleFor(x => x.Password)
                       .NotEmpty();
                   
        }
    }
}
