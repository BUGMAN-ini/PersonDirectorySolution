using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class PhoneNumberRepository : Repository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(AppDbContext db) : base(db)
        {
        }

        public async Task AddRangeAsync(IEnumerable<PhoneNumber> phones)
        {
            await _dbSet.AddRangeAsync(phones);
        }
    }
}
