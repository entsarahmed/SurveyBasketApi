namespace SurveyBasket.Api.Services
{
    public class WindowsOsService:IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public string OperationId { get; }
        public WindowsOsService()
        {
            OperationId = Guid.NewGuid().ToString()[^4..];
        }

        public string RunApp()
        {
            return "Running from windows";
        }
    }
}
