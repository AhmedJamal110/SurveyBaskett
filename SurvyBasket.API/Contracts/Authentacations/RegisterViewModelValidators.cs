namespace SurveyBasket.API.Contracts.Authentacations
{
    public class RegisterViewModelValidators : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidators()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(3, 100);

            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();


            RuleFor(x => x.Password)
            .NotEmpty();

        }
    }
}
