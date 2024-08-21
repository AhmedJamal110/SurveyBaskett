using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.API.Configrations
{
    public class PollConfigration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasIndex(x => x.Title).IsUnique();
            builder.Property(x => x.Title).HasMaxLength(1500);
            builder.Property(x => x.Summary).HasMaxLength(1500);
        }
    }
}
