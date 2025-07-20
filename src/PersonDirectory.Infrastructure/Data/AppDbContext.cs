using PersonDirectory.Domain.Enums;

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
                new City { Id = 1, Name = "Tbilisi" , Country = "Georgia"},
                new City { Id = 2, Name = "Batumi" , Country = "Georgia" },
                new City { Id = 3, Name = "Kutaisi", Country = "Georgia" },
                new City { Id = 4, Name = "Rustavi" , Country = "Georgia" },
                new City { Id = 5, Name = "Zugdidi" , Country = "Georgia" }
            );

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FirstName = "Luka", LastName = "Matiashvili", Gender = Gender.Male, PersonalNumber = "01010101010", DateOfBirth = new DateTime(1990, 1, 1), CityId = 1 , ImagePath = "C://Users/Documents/Image"},
                new Person { Id = 2, FirstName = "Nino", LastName = "Beridze", Gender = Gender.Female, PersonalNumber = "02020202020", DateOfBirth = new DateTime(1992, 2, 2), CityId = 2, ImagePath = "C://Users/Documents/Image" },
                new Person { Id = 3, FirstName = "Giorgi", LastName = "Kiknavelidze", Gender = Gender.Male, PersonalNumber = "03030303030", DateOfBirth = new DateTime(1993, 3, 3), CityId = 3, ImagePath = "C://Users/Documents/Image" },
                new Person { Id = 4, FirstName = "Ana", LastName = "Chikvaidze", Gender = Gender.Female, PersonalNumber = "04040404040", DateOfBirth = new DateTime(1994, 4, 4), CityId = 4, ImagePath = "C://Users/Documents/Image" },
                new Person { Id = 5, FirstName = "Dato", LastName = "Gachechiladze", Gender = Gender.Male, PersonalNumber = "05050505050", DateOfBirth = new DateTime(1995, 5, 5), CityId = 5, ImagePath = "C://Users/Documents/Image" }
            );

            modelBuilder.Entity<RelatedPerson>().HasData(
                new RelatedPerson { Id = 1, PersonId = 1, RelatedToPersonId = 2, RelationType = RelationType.Friend },
                new RelatedPerson { Id = 2, PersonId = 1, RelatedToPersonId = 3, RelationType = RelationType.Colleague },
                new RelatedPerson { Id = 3, PersonId = 2, RelatedToPersonId = 4, RelationType = RelationType.Spouse },
                new RelatedPerson { Id = 4, PersonId = 3, RelatedToPersonId = 5, RelationType = RelationType.Other },
                new RelatedPerson { Id = 5, PersonId = 4, RelatedToPersonId = 1, RelationType = RelationType.Friend }
            );
        }
    }
}
