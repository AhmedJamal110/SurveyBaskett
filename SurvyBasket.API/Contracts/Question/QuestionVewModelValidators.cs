namespace SurveyBasket.API.Contracts.Question
{
    public class QuestionVewModelValidators : AbstractValidator<QuestionVewModel>
    {
        public QuestionVewModelValidators()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(x => x.Answers)
                .Must(x => x.Count > 1)
                .WithMessage("Answers should be more than 1 ");


            RuleFor(x => x.Answers)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("you can not add deplucated answers");
        }
    }
}
