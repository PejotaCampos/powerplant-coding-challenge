using PowerPlantChallenge.Models;

namespace PowerPlantChallenge.Services
{
    public interface IPowerplantCostService
    {
        public (Powerplant powerplant, double load) VerifyBestCost(IEnumerable<Powerplant> powerplants, Fuels fuels, double load);
    }

    public class PowerplantCostService : IPowerplantCostService
    {
        private const double co2TonByEnergy = 0.3;

        public (Powerplant powerplant, double load) VerifyBestCost(IEnumerable<Powerplant> powerplants, Fuels fuels, double load)
        {
            var powerplantsArray = powerplants.ToArray();
            var bestPowerplant = powerplantsArray[0];
                
            var fuelCost = GetFuelCost(bestPowerplant.Type, fuels);

            var bestCost = load > bestPowerplant.PowerMin ? load * fuelCost : bestPowerplant.PowerMin * fuelCost;

            var bestLoad = load;

            for (int i = 1; i < powerplantsArray.Length; i++)
            {
                var currentPowerplant = powerplantsArray[i];

                fuelCost = GetFuelCost(currentPowerplant.Type, fuels);

                var powerLoaded = load > currentPowerplant.PowerMin ? load : currentPowerplant.PowerMin;

                var actualCost = powerLoaded * fuelCost;

                if (actualCost < bestCost)
                {
                    bestCost = actualCost;
                    bestLoad = powerLoaded;
                    bestPowerplant = currentPowerplant;
                }
            }

            return (bestPowerplant, bestLoad);
        }

        private static double GetFuelCost(string type, Fuels fuels)
        {
            var isGasfired = type == "gasfired";

            var fuelCost = isGasfired ? (fuels.GasEuroMWh + fuels.Co2EuroTon * co2TonByEnergy)
                : fuels.KerosineEuroMWh;

            return fuelCost;
        }
    }
}
