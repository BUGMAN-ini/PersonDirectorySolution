namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }
        IRelatedPersonRepository RelatedPersons { get; }
        ICityRepository City { get; }   
        IPhoneNumberRepository PhoneNumber { get; }

        Task<int> SaveChangesAsync();
    }
}
