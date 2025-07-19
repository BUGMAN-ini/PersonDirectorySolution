namespace PersonDirectory.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext _context,IPersonRepository _person)
        : IUnitOfWork
    {
        public IPersonRepository Persons { get; } = _person;

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
