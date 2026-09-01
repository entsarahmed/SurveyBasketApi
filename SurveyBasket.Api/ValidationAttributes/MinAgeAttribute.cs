using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MinAgeAttribute(int MinAge):ValidationAttribute
    {
        private readonly int _minAge = MinAge;
       protected  override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           if(value is not null)
            {
                var date = (DateTime)value;
                if (DateTime.Today < date.AddYears(_minAge))
                    return new ValidationResult($"Invalid {validationContext.DisplayName} Age should be {_minAge} years old.");
            }
            return ValidationResult.Success;
        }
    }
}
