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

        public Task<IEnumerable<RelatedPerson>> GetByPersonIdAsync(int personId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(RelatedPerson entity)
        {
            throw new NotImplementedException();
        }
    }
}
