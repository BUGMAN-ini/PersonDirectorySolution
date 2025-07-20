namespace PersonDirectory.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public IPersonRepository Person { get; private set; }

        public IRelatedPersonRepository RelatedPerson { get; private set; }

        public ICityRepository City { get; private set; }

        public UnitOfWork(AppDbContext db, IPersonRepository person
            ,IRelatedPersonRepository relatedperson,ICityRepository city)
        {
            _db = db;
            Person = new PersonRepository(_db);
            RelatedPerson = new RelatedPersonRepository(_db);
            City = new CityRepository(_db);
        }

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
