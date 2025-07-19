namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; }
        Task<int> SaveChangesAsync();
    }
}
