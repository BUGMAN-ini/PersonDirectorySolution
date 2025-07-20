namespace PersonDirectory.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => 
           await dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public void Remove(T entity) => dbSet.Remove(entity);

        public void Update(T entity) => dbSet.Update(entity);
    }
}
