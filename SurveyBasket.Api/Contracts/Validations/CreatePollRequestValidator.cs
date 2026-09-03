namespace SurveyBasket.Api.Contracts.Validations
{
    public class CreatePollRequestValidator:AbstractValidator<CreatePollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .Length(3, 100);
            //.MinimumLength(3)
            // .MaximumLength(100);

            RuleFor(x => x.Summary)
                .NotEmpty()
                .Length(3, 1500);
        }
    }
}
