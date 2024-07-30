using System.Text.Json.Serialization;

namespace SpacexWebApp.Server.Data.Models
{
    public class Launchpad
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name {  get; set; }
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
    }
}
