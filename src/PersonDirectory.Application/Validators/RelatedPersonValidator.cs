namespace PersonDirectory.Application.Validators
{
    public class RelatedPersonValidator : AbstractValidator<CreateRelatedPersonDTO>
    {
        public RelatedPersonValidator()
        {
            RuleFor(x => x.RelationType)
                .IsInEnum();

            RuleFor(x => x.RelatedToPersonId)
                .GreaterThan(0);
        }
    }
}
