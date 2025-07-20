using PersonDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.DTOs
{
    public class RelatedPersonDTO
    {
        public RelationType RelationType { get; set; }
        public int RelatedToPersonId { get; set; }
    }
}
