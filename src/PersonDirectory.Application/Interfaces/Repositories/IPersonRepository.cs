using PersonDirectory.Infrastructure.Repositories.Interfaces;

namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<List<Person>> GetAllWithRelationsAsync();
        Task<bool> PinExistsAsync(string personalNumber);
        Task<Person> GetByIdDetailAsync(int id);
    }
}
