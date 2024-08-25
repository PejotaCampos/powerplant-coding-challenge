using System.Text.Json.Serialization;

namespace PowerPlantChallenge.Models
{
    public class Powerplant
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        [JsonPropertyName("efficiency")]
        public double Efficiency { get; set; }

        [JsonPropertyName("pmin")]
        public int PowerMin { get; set; }

        [JsonPropertyName("pmax")]
        public int PowerMax { get; set; }

        public bool IsOn { get; set; } = false;

        public double PowerLoaded { get; set; }
    }
}
