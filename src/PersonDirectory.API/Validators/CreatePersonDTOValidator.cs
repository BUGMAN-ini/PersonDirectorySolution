using FluentValidation;
using Microsoft.Extensions.Localization;
using PersonDirectory.API.Resources;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Repositories;
using System.Text.RegularExpressions;

namespace PersonDirectory.Application.Validators
{
    public class CreatePersonDTOValidator : AbstractValidator<CreatePersonDTO>
    {
        public CreatePersonDTOValidator(IPersonRepository repo, IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .Length(2, 50).WithMessage(localizer["FirstNameLength"])
                .Must(BeOnlyGeorgianOrLatin)
                    .WithMessage(localizer["FirstNameGeorgianOrLatin"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .Length(2, 50).WithMessage(localizer["LastNameLength"])
                .Must(BeOnlyGeorgianOrLatin)
                    .WithMessage(localizer["LastNameGeorgianOrLatin"]);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(localizer["GenderInvalid"]);

            RuleFor(x => x.PersonalNumber)
                .Matches(@"^\d{11}$")
                .WithMessage("Must be exactly 11 digits.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(localizer["DateOfBirthRequired"])
                .Must(BeAtLeast18).WithMessage(localizer["PersonMustBeAdult"]);
        }

        private bool BeOnlyGeorgianOrLatin(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            var isGeorgian = Regex.IsMatch(name, @"^[ა-ჰ]+$");
            var isLatin = Regex.IsMatch(name, @"^[a-zA-Z]+$");

            return isGeorgian || isLatin;
        }
        private bool BeAtLeast18(DateTime birthDate)
        {
            return birthDate <= DateTime.Today.AddYears(-18);
        }
    }
}
