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

        [HttpPost(Name = "GetWeatherForecast")]
        public IEnumerable<EnergyResponse> Get(EnergyRequest request)
        {
            return _meritOrderService.SwitchPowerplants(request);
        }
    }
}
