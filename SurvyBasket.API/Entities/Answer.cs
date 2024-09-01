namespace SurveyBasket.API.Entities
{
    public sealed class Answer : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public int QuestionId { get; set; }
         public Question Question { get; set; } = default!;

    }
}
