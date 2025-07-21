using FluentValidation;
using PersonDirectory.Application.DTOs;
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
                .IsInEnum().WithMessage("Non-Existing Type");

            RuleFor(x => x.Number)
                .NotEmpty()
                .Length(4, 50)
                .WithMessage("Number is Neccesary");
        }
    }
}
