using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Exceptions
{
    public class DuplicatePersonalNumberException : Exception
    {
        public DuplicatePersonalNumberException(string personalNumber)
        : base($"Personal number '{personalNumber}' already exists.") { }
    }
}
