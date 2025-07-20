namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }
        IRelatedPersonRepository RelatedPerson { get; }
        ICityRepository City { get; }   

        Task<int> SaveChangesAsync();
    }
}
