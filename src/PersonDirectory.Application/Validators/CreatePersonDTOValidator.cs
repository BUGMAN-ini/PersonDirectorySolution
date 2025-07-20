using System.Text.RegularExpressions;

namespace PersonDirectory.Application.Validators
{
    public class CreatePersonDTOValidator : AbstractValidator<CreatePersonDTO>
    {
        public CreatePersonDTOValidator(IPersonService serv)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().Length(2, 50).Must(SingleAlphabet);
            RuleFor(x => x.LastName)
                .NotEmpty().Length(2, 50).Must(SingleAlphabet);

            RuleFor(x => x.PersonalNumber)
                .Matches(@"^\d{11}$")
                .MustAsync(async (pn, _) => !await serv.PinExistsAsync(pn))
                .WithMessage("Personal number already exists.");

            RuleFor(x => x.DateOfBirth)
                .Must(d => d <= DateTime.Now.AddYears(-18))
                .WithMessage("Must be at least 18 years old.");

            RuleFor(x => x.CityId).GreaterThan(0);

            When(x => x.PhoneNumbers is not null, () =>
                RuleForEach(x => x.PhoneNumbers!).SetValidator(new PhoneNumberValidator()));

            RuleFor(x => x.ImageFile)
                .Must(f => f == null || f.Length <= 1_000_000)
                .WithMessage("Image too large (max 1 MB).")
                .Must(f => f == null || f.ContentType.StartsWith("image/"))
                .WithMessage("File must be an image.");

            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("Image file is required.")
                .Must(f => f.ContentType.StartsWith("image/"))
                .WithMessage("File must be an image.");
        }

        private bool SingleAlphabet(string s)
            => Regex.IsMatch(s, @"^[ა-ჰ]+$") || Regex.IsMatch(s, @"^[a-zA-Z]+$");
    }
}
