using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class RelatedPersonRepository : Repository<RelatedPerson>, IRelatedPersonRepository
    {
        public RelatedPersonRepository(AppDbContext db) : base(db)
        {
        }

        public async Task AddRangeAsync(IEnumerable<RelatedPerson> relations)
        {
           await _dbSet.AddRangeAsync(relations);
        }
    }
}
