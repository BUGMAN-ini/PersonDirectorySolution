using PersonDirectory.Application.DTOs;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PersonRepository(AppDbContext context) 
        : Repository<Person>(context), IPersonRepository
    {
        private readonly DbSet<Person> _dbSet = context.Set<Person>();

        public async Task AddPersonAsync(Person person)
        {
            await _dbSet.AddAsync(person);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeletePersonAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditRelatedPerson()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAllPersonsWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PersonDTO> GetByIdDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Person?> GetPersonWithDetailsAsync(int id)
        {
            return await _dbSet
               .Include(p => p.PhoneNumbers)
               .Include(p => p.City)
               .Include(p => p.RelatedPersons)
                   .ThenInclude(rp => rp.RelatedToPerson)
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<IEnumerable<Person>> SearchAsync(string? name, string? lastName, string? personalNumber)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, UpdatePersonDTO person)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePersonAsync(int id, PersonDTO person)
        {
            throw new NotImplementedException();
        }
    }
}
