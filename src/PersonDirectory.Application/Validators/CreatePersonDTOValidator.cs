using System.Text.RegularExpressions;

namespace PersonDirectory.Application.Validators
{
    public class CreatePersonDTOValidator : AbstractValidator<CreatePersonDTO>
    {
        public CreatePersonDTOValidator(IPersonRepository repo)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().Length(2, 50)
                .Must(text => Regex.IsMatch(text, @"^[ა-ჰ]+$") || Regex.IsMatch(text, @"^[a-zA-Z]+$"))
                .WithMessage("First name must be Georgian OR Latin only.");

            RuleFor(x => x.LastName)
                .NotEmpty().Length(2, 50)
                .Must(text => Regex.IsMatch(text, @"^[ა-ჰ]+$") || Regex.IsMatch(text, @"^[a-zA-Z]+$"))
                .WithMessage("Last name must be Georgian OR Latin only.");

            RuleFor(x => x.PersonalNumber)
                .Matches(@"^\d{11}$").WithMessage("Must be exactly 11 digits.")
                .MustAsync(async (pn, ct) => !await repo.PinExistsAsync(pn))
                .WithMessage("Personal number already exists.");

            RuleFor(x => x.DateOfBirth)
                .Must(d => d <= DateTime.UtcNow.AddYears(-18))
                .WithMessage("Person must be at least 18 years old.");

            RuleFor(x => x.CityId).GreaterThan(0);

            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new PhoneNumberValidator());

            RuleForEach(x => x.RelatedPersons)
                .SetValidator(new RelatedPersonValidator());
        }
    }
}
