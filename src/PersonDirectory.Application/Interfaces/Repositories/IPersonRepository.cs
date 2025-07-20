using PersonDirectory.Infrastructure.Repositories.Interfaces;

namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<List<Person>> GetAllWithRelationsAsync();
    }
}
