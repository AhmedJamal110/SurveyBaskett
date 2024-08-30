using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.API.Configrations
{
    public class ApplicationUserConfigration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserID");

            builder.Property(x => x.FirstName)
                    .HasMaxLength(100);



            builder.Property(x => x.LastName)
                    .HasMaxLength(100);
        }
    }
}
