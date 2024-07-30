using System.Text.Json.Serialization;

namespace SpacexWebApp.Server.Data.Models
{
    public class Rocket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("boosters")]
        public int Boosters { get; set; }
        [JsonPropertyName("cost_per_launch")]
        public int CostPerLaunch { get; set; }
        [JsonPropertyName("success_rate_pct")]
        public int SuccessRate { get; set; }
        public string Company { get; set; }
        public string Wikipedia { get; set; }
        public string Description { get; set; }        
    }
}
