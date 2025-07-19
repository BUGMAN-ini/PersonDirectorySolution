using PersonDirectory.Application.Interfaces.Repositories;

namespace PersonDirectory.Application.Services
{
    public class PersonService
        (IUnitOfWork unitofwork,IMapper mapper) : IPersonService
    {
        public async Task<PersonDTO> CreatePersonAsync(CreatePersonDTO dto)
        {
            var person = mapper.Map<Person>(dto);
            await unitofwork.Persons.AddAsync(person);
            await unitofwork.SaveChangesAsync();
            return mapper.Map<PersonDTO>(person);
        }

        public async Task DeletePersonAsync(int id)
        {
            unitofwork.Persons.DeleteAsync(id);
            await unitofwork.SaveChangesAsync();
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var person = await unitofwork.Persons.GetByIdDetailAsync(id);
            var personDto = mapper.Map<PersonDTO>(person);
            return personDto;
        }

        public async Task<IEnumerable<PersonDTO>> SearchAsync(string? name, string? lastName, string? personalNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonDTO> UpdatePersonAsync(int id, UpdatePersonDTO dto)
        {
            var person = await unitofwork.Persons.GetByIdDetailAsync(id);
            if (person is not null)
            {
                await unitofwork.Persons.UpdateAsync(id,dto);
                await unitofwork.SaveChangesAsync();
                return mapper.Map<PersonDTO>(person);
            }

            return null;
        }
    }
}
