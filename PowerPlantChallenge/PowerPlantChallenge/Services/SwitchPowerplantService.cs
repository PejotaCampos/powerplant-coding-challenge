using PowerPlantChallenge.Models;

namespace PowerPlantChallenge.Services
{
    public interface ISwitchPowerplantService
    {
        public IEnumerable<Powerplant> TurnOnFuelsNoCost(IEnumerable<Powerplant> powerplants, double wind);

        public IEnumerable<Powerplant> TurnOnByCost(IEnumerable<Powerplant> powerplants, Fuels fuels, double load);

        public double EnergyLoaded(IEnumerable<Powerplant> powerplants);

        public IEnumerable<EnergyResponse> GetResponse(IEnumerable<Powerplant> powerplants);
    }

    public class SwitchPowerplantService(IPowerplantCostService powerplantCostService) : ISwitchPowerplantService
    {
        private const string Wind = "windturbine";

        private readonly IPowerplantCostService powerPlantCostService = powerplantCostService;

        public double EnergyLoaded(IEnumerable<Powerplant> powerplants) => powerplants.Sum(p => p.PowerLoaded);
        

        public IEnumerable<EnergyResponse> GetResponse(IEnumerable<Powerplant> powerplants)
        {
            List<EnergyResponse> response = new();

            var powerplantsOrdered = powerplants.OrderByDescending(p => p.Efficiency)
                .ThenBy(p=>p.Name)
                .ToArray();

            foreach (var powerplant in powerplantsOrdered)
            {
                response.Add(new(){ Name = powerplant.Name, Energy = Math.Round(powerplant.PowerLoaded, 1) } );
            }

            return response;
        }

        public IEnumerable<Powerplant> TurnOnFuelsNoCost(IEnumerable<Powerplant> powerplants, double wind)
        {
            var powerplantsWind = powerplants.Where(p => p.Type == Wind).ToList();

            var windRate = wind / 100;

            foreach (var powerplantWind in powerplantsWind)
            {
                var powerLoaded = powerplantWind.PowerMax * windRate;
                powerplants.FirstOrDefault(p => p.Name == powerplantWind.Name).PowerLoaded = powerplantWind.PowerMax * windRate; ;
                powerplants.FirstOrDefault(p => p.Name == powerplantWind.Name).IsOn = true;
            }

            return powerplants;
        }

        public IEnumerable<Powerplant> TurnOnByCost(IEnumerable<Powerplant> powerplants, Fuels fuels, double load)
        {
            var powerplantsOff = powerplants.Where(p => !p.IsOn )
                .OrderByDescending(p => p.Efficiency) //this should retrieve gasfired first
                .ToList();

            if (powerplantsOff.Count == 0) return powerplants;

            List<Powerplant> powerplantsOn = new();

            foreach (var powerplant in powerplantsOff)
            {
                if(load < powerplant.PowerMin)
                {
                    var powerplantsWhichStillOff = powerplants.Where(p => !p.IsOn).ToList();
                    var bestPowerplantCost = powerPlantCostService.VerifyBestCost(powerplantsWhichStillOff, fuels, load);

                    var bestPowerplant = bestPowerplantCost.powerplant;
                    var bestLoad = bestPowerplantCost.load;

                    powerplants.FirstOrDefault(p => p.Name == bestPowerplant.Name).IsOn = true;
                    powerplants.FirstOrDefault(p => p.Name == bestPowerplant.Name).PowerLoaded = bestLoad;
                    return powerplants;
                }

                if (load < powerplant.PowerMax) 
                {
                    powerplants.FirstOrDefault(p => p.Name == powerplant.Name).IsOn = true;
                    powerplants.FirstOrDefault(p => p.Name == powerplant.Name).PowerLoaded = load;

                    return powerplants;
                }

                powerplants.FirstOrDefault(p => p.Name == powerplant.Name).IsOn = true;
                powerplants.FirstOrDefault(p => p.Name == powerplant.Name).PowerLoaded = powerplant.PowerMax;
                load = load - powerplant.PowerMax;
            }

            return powerplants;
        }
    }
}
