using PowerPlantChallenge.Models;

namespace PowerPlantChallenge.Services
{
    public interface IPowerplantCostService
    {
        public (Powerplant powerplant, double load) VerifyBestCost(IEnumerable<Powerplant> powerplants, Fuels fuels, double load);
    }

    public class PowerplantCostService : IPowerplantCostService
    {
        public (Powerplant powerplant, double load) VerifyBestCost(IEnumerable<Powerplant> powerplants, Fuels fuels, double load)
        {
            var powerplantsArray = powerplants.ToArray();
            var bestPowerplant = powerplantsArray[0];

            var fuelCost = bestPowerplant.Type == "gasfired" ? fuels.GasEuroMWh : fuels.KerosineEuroMWh;

            var bestCost = load > bestPowerplant.PowerMin ? load * fuelCost : bestPowerplant.PowerMin * fuelCost;

            var bestLoad = load;

            for(int i = 1; i < powerplantsArray.Length; i++)
            {
                var currentPowerplant = powerplantsArray[i];

                fuelCost = currentPowerplant.Type == "gasfired" ? fuels.GasEuroMWh : fuels.KerosineEuroMWh;          

                var powerLoaded = load > currentPowerplant.PowerMin ? load : currentPowerplant.PowerMin;

                var actualCost = powerLoaded * fuelCost;

                if(actualCost < bestCost)
                {
                    bestCost = actualCost;
                    bestLoad = powerLoaded;
                    bestPowerplant = currentPowerplant;
                }
            }

            return (bestPowerplant, bestLoad);
        }
    }
}
