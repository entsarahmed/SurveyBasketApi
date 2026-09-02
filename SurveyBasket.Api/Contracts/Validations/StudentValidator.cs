namespace SurveyBasket.Api.Contracts.Validations
{
    public class StudentValidator:AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.DateOfBirth)
                .Must(BeMoreThan18Years)//(x => DateTime.Today > x!.Value.AddYears(18))
                .When(x => x.DateOfBirth.HasValue)
                .WithMessage("{PropertyName} is invalid, age should be 18 years at least");
        }

        private bool BeMoreThan18Years(DateTime? dateOfBirth)
        {
            return DateTime.Today > dateOfBirth!.Value.AddYears(18);
        }
    }
}
