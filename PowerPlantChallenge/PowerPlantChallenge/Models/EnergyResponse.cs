using Newtonsoft.Json;

namespace PowerPlantChallenge.Models
{
    public class EnergyResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("p")]
        public double Energy { get; set; }
    }
}
