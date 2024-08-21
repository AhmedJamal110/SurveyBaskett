namespace SurveyBasket.API.Persistence
{
    public class ApplicationDbContect : DbContext
    {
        public ApplicationDbContect(DbContextOptions<ApplicationDbContect> option) : base(option)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
