namespace SurveyBasket.API.Errors
{
    public class QuestionErrors
    {

        public static readonly Error QuestionNotFound
        = new("Question.NotFound", "Question not found");

        public static readonly Error QuestionDeplucated
          = new("Question.QuestionIsAlreadyExsit", "Question title is alredy Exist");

    }
}
