using PersonDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.DTOs
{
    public class CreateRelatedPersonDTO
    {
        public RelationType RelationType { get; set; }
        public int RelatedToPersonId { get; set; }
    }
}
