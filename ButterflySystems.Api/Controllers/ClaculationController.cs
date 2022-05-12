using ButterflySystems.Api.Core.Models;
using ButterflySystems.Api.Core.Services;
using ButterflySystems.EntityDataContracts.ResponseDataModels;
using ButterflySystems.ViewDataContracts.RequestDataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ButterflySystems.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaculationController : ControllerBase
    {
        private readonly ILogger<ClaculationController> _logger;
        private readonly CalculationService _calculationService;

        public ClaculationController(ILogger<ClaculationController> logger, CalculationService calculationService)
        {
            _logger = logger;
            _calculationService = calculationService;
        }

        // I have put the exception handling in controller as I want to bubble up all exceptions and return a
        // standard InternalServerError response.
        [ProducesResponseType(typeof(CalculationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost("Calculate")]
        [AllowAnonymous]
        public async Task<ActionResult> CalculateAsync([FromBody] CalculationRequest request)
        {
            try
            {
                return Ok(await _calculationService.CalculateResultAsync(request));
            }
            catch (Exception e)
            {
                _logger.LogError($"CalculationController > CalculateAsync > Calculation Exception:"+Environment.NewLine+
                                 e.Message+Environment.NewLine+
                                 e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}