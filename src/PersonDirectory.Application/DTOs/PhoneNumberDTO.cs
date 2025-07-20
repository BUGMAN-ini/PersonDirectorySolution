using PersonDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.DTOs
{
    public class PhoneNumberDTO
    {
        public string Number { get; set; } = null!;
        public PhoneNumberType Type { get; set; } = PhoneNumberType.Mobile;
    }
}
