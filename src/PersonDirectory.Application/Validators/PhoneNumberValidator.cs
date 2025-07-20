using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Validators
{
    public class PhoneNumberValidator : AbstractValidator<PhoneNumberDTO>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("ტიპი არასწორია.");

            RuleFor(x => x.Number)
                .NotEmpty().Length(4, 50);
        }
    }
}
