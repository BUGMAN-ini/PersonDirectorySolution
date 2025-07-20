using PersonDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Domain.Entity
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public PhoneNumberType Type { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}
