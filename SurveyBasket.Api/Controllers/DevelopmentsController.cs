using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentsController : ControllerBase
    {
        private readonly ILogger _logger;

        public DevelopmentsController(ILogger<DevelopmentsController> logger)
        {
            _logger = logger;
        }

        //private readonly IOperationTransient _operationTransient;
        //private readonly IOperationScoped _operationScoped;
        //private readonly IOperationSingleton _operationSingleton;
        //public DevelopmentsController(IOperationTransient operationTransient, IOperationScoped operationScoped, IOperationSingleton operationSingleton, ILogger<DevelopmentsController> logger)
        //{
        //    _operationTransient = operationTransient;
        //    _operationScoped = operationScoped;
        //    _operationSingleton = operationSingleton;
        //    _logger = logger;
        //}

        [HttpGet]
        public IActionResult Run([FromKeyedServices("windows")] IOperationTransient windowsService,
            [FromKeyedServices("macOs")] IOperationTransient macOsService)
        {
          // _logger.LogInformation("Transient: {OperationId}", _operationTransient.OperationId);
          //_logger.LogInformation("Scoped: {OperationId}", _operationScoped.OperationId);
          // _logger.LogInformation("Singleton: {OperationId}", _operationSingleton.OperationId);

            _logger.LogWarning("Transient: {OperationId}", windowsService.OperationId);
            _logger.LogError("MacOs {0}", macOsService.OperationId);
            return Ok();
        }
    }
}
