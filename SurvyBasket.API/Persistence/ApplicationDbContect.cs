

using System.Security.Claims;

namespace SurveyBasket.API.Persistence
{
    public class ApplicationDbContect : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContect(DbContextOptions<ApplicationDbContect> option, IHttpContextAccessor httpContextAccessor )
            : base(option)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFk = modelBuilder.Model
                                                        .GetEntityTypes()
                                                        .SelectMany(x => x.GetForeignKeys())
                                                        .Where(x => x.DeleteBehavior == DeleteBehavior.Cascade && !x.IsOwnership);

            foreach (var FK in cascadeFk)
                FK.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities =  ChangeTracker.Entries<BaseEntity>();

            var currentUser = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                
            foreach (var entity in entities)
            {

                if (entity.State == EntityState.Added)
                {
                    entity.Property(x => x.CreatedById).CurrentValue = currentUser;
                   
                }
                else if(entity.State == EntityState.Modified)
                {
                    entity.Property(x => x.UpdatedById).CurrentValue = currentUser;
                    entity.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

    }
}
