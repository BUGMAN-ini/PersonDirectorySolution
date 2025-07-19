namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        Task UpdateAsync(int id, UpdatePersonDTO person);
        Task DeleteAsync(int id);
        Task<PersonDTO> GetByIdDetailAsync(int id);

    }
}
