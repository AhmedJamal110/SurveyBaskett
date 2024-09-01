namespace SurveyBasket.API.Contracts.Question
{
    public record QuestionVewModel
    (
        string Content,
        ICollection<string> Answers
     );
}
