using PersonDirectory.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Interfaces.Repositories
{
    public interface IPhoneNumberRepository : IRepository<PhoneNumber>
    {
        Task AddRangeAsync(IEnumerable<PhoneNumber> phones);
    }
}
