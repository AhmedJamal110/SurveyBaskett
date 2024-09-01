using SurveyBasket.API.Contracts.Answer;

namespace SurveyBasket.API.Contracts.Question
{
    public record QuestionDto
        (
            int ID,
            string Content,
            IEnumerable<AnswerDto> Answers
        );
}
