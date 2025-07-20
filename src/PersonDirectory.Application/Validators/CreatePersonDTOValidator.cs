using System.Text.RegularExpressions;

namespace PersonDirectory.Application.Validators
{
    public class CreatePersonDTOValidator : AbstractValidator<CreatePersonDTO>
    {
        public CreatePersonDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("სახელი სავალდებულოა.")
                .Length(2, 50)
                .Must(BeOnlyGeorgianOrLatin).WithMessage("სახელი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ანბანს.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("გვარი სავალდებულოა.")
                .Length(2, 50)
                .Must(BeOnlyGeorgianOrLatin).WithMessage("გვარი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ანბანს.");

            RuleFor(x => x.PersonalNumber)
                .NotEmpty()
                .Matches(@"^\d{11}$").WithMessage("პირადი ნომერი უნდა იყოს ზუსტად 11 ციფრი.");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAtLeast18).WithMessage("მომხმარებელი უნდა იყოს მინიმუმ 18 წლის.");

            RuleFor(x => x.CityId)
                .GreaterThan(0);

            RuleForEach(x => x.PhoneNumbers).SetValidator(new PhoneNumberValidator());
        }

        private bool BeOnlyGeorgianOrLatin(string name)
        {
            var geo = @"^[ა-ჰ]+$";
            var lat = @"^[a-zA-Z]+$";
            return Regex.IsMatch(name, geo) || Regex.IsMatch(name, lat);
        }

        private bool BeAtLeast18(DateTime dob) => dob <= DateTime.UtcNow.AddYears(-18);
    }
}
