using PersonDirectory.Application.DTOs;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly AppDbContext _db;

        public PersonRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Person>> GetAllWithRelationsAsync()
        {
            return await _db.Persons
            .Include(p => p.RelatedPersons)
            .ThenInclude(rp => rp.RelatedToPerson)
            .ToListAsync();
        }

        public async Task<Person> GetByIdDetailAsync(int id)
        {
           return await _db.Persons
                    .Include(p => p.City)
                    .Include(p => p.PhoneNumbers)
                    .Include(p => p.RelatedPersons)
                        .ThenInclude(rp => rp.RelatedToPerson)
                    .FirstAsync(p => p.Id == id);
        }

        public async Task<bool> PinExistsAsync(string personalNumber)
        {
            return await _db.Persons
                .AnyAsync(p => p.PersonalNumber == personalNumber);
        }
    }
}
