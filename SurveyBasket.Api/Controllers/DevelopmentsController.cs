using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentsController : ControllerBase
    {
        private readonly IOS _windowsOsService;

        public DevelopmentsController(IOS windowsOsService)
        {
            _windowsOsService = windowsOsService;
        }

        [HttpGet]
        public IActionResult Run()
        {
          //  var os = new WindowsOsService();

            ///var message = os.RunApp();
            var message = _windowsOsService.RunApp();

            return Ok(message);

        }
    }
}
