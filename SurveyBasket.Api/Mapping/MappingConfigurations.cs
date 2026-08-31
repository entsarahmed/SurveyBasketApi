using Mapster;
using SurveyBasket.Api.Contracts.Response;
using System.Net.Cache;

namespace SurveyBasket.Api.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentResponse>().TwoWays();
              // .Map(dest => dest.FullName, src => $"{src.FirstName} {src.MiddleName} {src.LastName}")
              // .Map(dest => dest.Age, src => DateTime.Now.Year - src.DateOfBirth!.Value.Year,
              // srcCond => srcCond.DateOfBirth.HasValue);
                  //.Map(dest => dest.DepartmentName, src => src.Department.Name);
                  //.Ignore(dest => dest.DepartmentName);
                 }
    }
}
