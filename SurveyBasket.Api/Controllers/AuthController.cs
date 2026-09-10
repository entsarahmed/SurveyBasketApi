using Microsoft.Extensions.Options;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService  authService, 
        IOptions<JwtOptions> options ) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IOptions<JwtOptions> _options = options;
       

        [HttpPost("")]
        public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken) 
        {

            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);

            return authResult is null ? BadRequest("Invalid email/password") : Ok(authResult);

        }
        
    }
}
