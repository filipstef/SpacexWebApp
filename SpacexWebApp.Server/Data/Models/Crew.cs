using System.Text.Json.Serialization;

namespace SpacexWebApp.Server.Data.Models
{
    public class Crew
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("agency")]
        public string Agency { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("wikipedia")]
        public string Wikipedia { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}

