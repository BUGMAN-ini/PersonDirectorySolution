namespace PersonDirectory.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
        public DbSet<RelatedPerson> RelatedPersons => Set<RelatedPerson>();
        public DbSet<City> Cities => Set<City>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Tbilisi", Country = "Georgia" },
                new City { Id = 2, Name = "Batumi", Country = "Georgia" },
                new City { Id = 3, Name = "Kutaisi", Country = "Georgia" });
        }
    }
}
