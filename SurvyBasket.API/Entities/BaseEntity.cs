namespace SurveyBasket.API.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public bool IsDelated { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; }


        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = default!;


        public string? UpdatedById { get; set; }
        public ApplicationUser? UpdatedBy { get; set; } 

    }
}
