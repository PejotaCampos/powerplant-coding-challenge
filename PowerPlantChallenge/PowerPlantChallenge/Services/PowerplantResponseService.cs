using PowerPlantChallenge.Models;

namespace PowerPlantChallenge.Services
{
    public interface IPowerplantResponseService
    {
        public IEnumerable<EnergyResponse> GetResponse(IEnumerable<Powerplant> powerplants);
    }

    public class PowerplantResponseService : IPowerplantResponseService
    {
        public IEnumerable<EnergyResponse> GetResponse(IEnumerable<Powerplant> powerplants)
        {
            var powerplantsOrdered = powerplants.OrderByDescending(p => p.Efficiency)
                .ThenBy(p => p.Name)
                .ToArray();

            List<EnergyResponse> response = new(powerplantsOrdered.Length);

            foreach (var powerplant in powerplantsOrdered)
            {
                response.Add(new() { Name = powerplant.Name, Energy = Math.Round(powerplant.PowerLoaded, 1) });
            }

            return response;
        }
    }
}
