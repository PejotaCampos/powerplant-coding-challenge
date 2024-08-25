using PowerPlantChallenge.Models;

namespace PowerPlantChallenge.Services
{
    public interface IMeritOrderService
    {
        public IEnumerable<EnergyResponse> SwitchPowerplants(EnergyRequest request);
    }

    public class MeritOrderService(ISwitchPowerplantService switchPowerplantService) : IMeritOrderService
    {
        private readonly ISwitchPowerplantService switchPowerplantService = switchPowerplantService;

        public IEnumerable<EnergyResponse> SwitchPowerplants(EnergyRequest request)
        {
            var noCostPowerOn = switchPowerplantService.TurnOnFuelsNoCost(request.Powerplants, request.Fuels.Wind);

            var energyLoaded = switchPowerplantService.EnergyLoaded(noCostPowerOn);

            var energyNeeded = request.Load - energyLoaded;

            if (energyNeeded <= 0)
                return switchPowerplantService.GetResponse(noCostPowerOn);

            var powerplantsTurnOn = switchPowerplantService.TurnOnByCost(noCostPowerOn, request.Fuels, energyNeeded);

            return switchPowerplantService.GetResponse(powerplantsTurnOn);
        }
    }
}
