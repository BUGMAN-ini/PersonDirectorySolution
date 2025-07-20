using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AppDbContext db) : base(db)
        {
            
        }


    }
}
