using SurveyBasket.API.Abstractions;

namespace SurveyBasket.API.Errors
{
    public static class PollErrors
    {
        public static readonly Error PollNotFound
            = new("Poll.NotFound", "Poll not found");

        public static readonly Error PollDeplucated
          = new("Poll.PollIsAlreadyExsit", "Poll title is alredy Exist");
        
     
    }
}
