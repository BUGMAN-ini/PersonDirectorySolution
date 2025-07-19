using PersonDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Entity
{
    public class RelatedPerson
    {
        public int Id { get; set; }

        [Required]
        public RelationType RelationType { get; set; }

        [Required]
        public int PersonId { get; set; }         
        public Person Person { get; set; }

        [Required]
        public int RelatedToPersonId { get; set; }
        public Person RelatedToPerson { get; set; }
    }
}
