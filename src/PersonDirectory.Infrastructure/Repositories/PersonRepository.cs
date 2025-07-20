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
            return await _db.Set<Person>()
                .Include(p => p.City)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPersons)
                    .ThenInclude(rp => rp.RelatedToPerson)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new KeyNotFoundException($"Person {id} not found");
        }

        public async Task<bool> PinExistsAsync(string personalNumber)
        {
            return await _db.Persons
                .AnyAsync(p => p.PersonalNumber == personalNumber);
        }
    }
}
