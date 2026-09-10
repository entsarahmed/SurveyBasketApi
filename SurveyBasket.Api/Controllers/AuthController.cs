using Microsoft.Extensions.Options;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService  authService, 
        IOptions<JwtOptions> options,
        IOptionsSnapshot<JwtOptions> optionsSnapshot,
        IOptionsMonitor<JwtOptions> optionsMonitor
        ) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IOptions<JwtOptions> _options = options;
        private readonly IOptionsSnapshot<JwtOptions> _optionsSnapshot = optionsSnapshot;
        private readonly IOptionsMonitor<JwtOptions> _optionsMonitor = optionsMonitor;
       

        [HttpPost("")]
        public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken) 
        {

            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

            return authResult is null ? BadRequest("Invalid email/password") : Ok(authResult);

        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
            var values = new
            {
                IOptionsValue = _options.Value.ExpiryMinutes,
                IOptionsSnapshotValue = _optionsSnapshot.Value.ExpiryMinutes,
                IOptionsMonitorValue = _optionsMonitor.CurrentValue.ExpiryMinutes
            };
            Thread.Sleep(5000);

            var values02 = new
            {
                IOptionsValue = _options.Value.ExpiryMinutes,
                IOptionsSnapshotValue = _optionsSnapshot.Value.ExpiryMinutes,
                IOptionsMonitorValue = _optionsMonitor.CurrentValue.ExpiryMinutes
            };
            return Ok(new {
                values,
                values02

});
        }
    }
}
