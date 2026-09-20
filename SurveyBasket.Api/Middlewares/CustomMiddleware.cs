namespace SurveyBasket.Api.Middlewares
{
    public class CustomMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public CustomMiddleware(ILogger<CustomMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Processing Request");
            await _next.Invoke(context);
            _logger.LogInformation("Processing Response");

        }
    }
}
