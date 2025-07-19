using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Enums
{
    public enum RelationType
    {
        Parent = 1,
        Child = 2,
        Sibling = 3,
        Spouse = 4,
        Friend = 5,
        Colleague = 6,
        Other = 7
    }
}
