namespace SurveyBasket.API.Entities
{
    public sealed class Question : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public int PollId { get; set; }
        public Poll Poll { get; set; } = default!;
        public ICollection<Answer> Answers { get; set; } = [];

    }
}
