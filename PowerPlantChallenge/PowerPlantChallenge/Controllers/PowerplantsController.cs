using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PowerPlantChallenge.Models;
using PowerPlantChallenge.Services;

namespace PowerPlantChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerplantsController : ControllerBase
    {

        private readonly ILogger<PowerplantsController> _logger;
        private readonly IMeritOrderService _meritOrderService;

        public PowerplantsController(ILogger<PowerplantsController> logger, IMeritOrderService meritOrderService)
        {
            _logger = logger;
            _meritOrderService = meritOrderService;
        }

        [HttpPost]
        public IActionResult Get(EnergyRequest request)
        {
            try
            {
                var powerplantsOn = _meritOrderService.SwitchPowerplants(request);

                var energyLoaded = powerplantsOn.Sum(p => p.Energy);

                if (request.Load > energyLoaded)
                    return BadRequest($"No enough powerplants to supply this load. The max energy generated is {energyLoaded}");

                return Ok(powerplantsOn);
            }
            catch (Exception e)
            {
                _logger.LogError("Error on switch powerplants process {erro}", e);
                return StatusCode(500);
            }
        }
    }
}
