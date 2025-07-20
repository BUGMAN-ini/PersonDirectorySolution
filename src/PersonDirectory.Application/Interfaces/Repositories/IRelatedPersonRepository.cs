using PersonDirectory.Infrastructure.Repositories.Interfaces;

namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IRelatedPersonRepository : IRepository<RelatedPerson>
    {
        Task AddRangeAsync(IEnumerable<RelatedPerson> relations);
    }
}
