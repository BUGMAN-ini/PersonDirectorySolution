using PersonDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Domain.Entity
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        [Required]
        public PhoneNumberType Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }

        [Required]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
