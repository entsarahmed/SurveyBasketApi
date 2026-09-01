using SurveyBasket.Api.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [MinAge(18),Display(Name ="Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public Department Department { get; set; } = default!;

    }
}
