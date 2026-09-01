namespace SurveyBasket.Api.Contracts.Request
{
    public record CreatePollRequest(
      // [Required(ErrorMessage = "Required Field!")] 
      //[AllowedValues("New", "Old", ErrorMessage ="Only 'New' and 'Old'  values are allowed")]
      //[MinLength(3)]
      //[MaxLength(100)]
     // [Length(3,100)]
      string Title,
      string Description
    );
}
