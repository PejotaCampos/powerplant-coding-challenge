namespace PowerPlantChallenge.Models
{
    public class EnergyRequest
    {
        public double Load { get; set; }

        public Fuels Fuels { get; set; }

        public List<Powerplant> Powerplants { get; set; }
    }
}
