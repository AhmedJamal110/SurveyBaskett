namespace SurveyBasket.API.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }

        public bool IsDelated { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; }

    }
}
