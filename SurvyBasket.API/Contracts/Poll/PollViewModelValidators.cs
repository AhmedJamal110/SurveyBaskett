using FluentValidation;

namespace SurveyBasket.API.Contracts.Poll
{
    public class PollViewModelValidators : AbstractValidator<PollViewModel>
    {
        public PollViewModelValidators()
        {
            RuleFor(x => x.Title)
                    .NotEmpty();


            RuleFor(x => x.Summary)
                    .NotEmpty();

            RuleFor(x => x.StartsAt)
                    .NotEmpty()
                    .GreaterThan(DateOnly.FromDateTime(DateTime.Today))
                    .WithMessage("Poll should strat greatrer than today");


            RuleFor(x => x.EndsAt)
                 .NotEmpty();

            RuleFor(x => x)
                    .Must(HasValidDate)
                    .WithName(nameof(PollViewModel.EndsAt))
                    .WithMessage("{PropertyName} Must be geater than or equal StartsAt ");


        }

        private bool HasValidDate(PollViewModel model)
           => model.EndsAt >= model.StartsAt;
    
    }

}
