using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Exceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException(int cityId)
        : base($"City with ID '{cityId}' was not found.") { }
    }
}
