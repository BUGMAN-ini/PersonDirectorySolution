using PersonDirectory.Application.Interfaces.Repositories;
using PersonDirectory.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PersonDirectory.Application.Services
{
    public class PersonService
        (IUnitOfWork unitofwork, IMapper mapper) : IPersonService
    {
        public async Task<PersonDTO> CreatePersonAsync(CreatePersonDTO dto)
        {
            var person = mapper.Map<Person>(dto);
            unitofwork.Person.AddAsync(person);
            await unitofwork.SaveChangesAsync();
            return mapper.Map<PersonDTO>(person);

        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await unitofwork.Person.GetByIdAsync(id);
            if (person == null)
            {
                throw new Exception($"Person with id {id} not found.");
            }
            unitofwork.Person.Remove(person);
            await unitofwork.SaveChangesAsync();

        }

        public async Task<IEnumerable<PersonDTO>> GetAllPersonAsync()
        {
            var persons = await unitofwork.Person.GetAllAsync();
            var personDTOs = mapper.Map<IEnumerable<PersonDTO>>(persons);
            return personDTOs;
        }

        public async Task<PersonDTO> GetById(int id)
        {
            var person = await unitofwork.Person.GetByIdDetailAsync(id);
            if (person == null)
            {
                throw new Exception($"Person with id {id} not found.");
            }
            return mapper.Map<PersonDTO>(person);

        }

        public Task<IEnumerable<PersonDTO>> SearchAsync(string? name, string? lastName, string? personalNumber)
        {
            throw new NotImplementedException();
        }

        public Task<PersonDTO> UpdatePersonAsync(int id, UpdatePersonDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
