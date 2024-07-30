using System.Text.Json.Serialization;

namespace SpacexWebApp.Server.Data.Models
{
    public class CustomLaunch
    {
        [JsonPropertyName("flight_number")]
        public int FlightNumber { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("date_utc")]
        public string DateUtc { get; set; }
        [JsonPropertyName("rocket")]
        public Rocket Rocket { get; set; }
        [JsonPropertyName("success")]
        public bool? Success { get; set; }
        [JsonPropertyName("details")]
        public string Details { get; set; }
        [JsonPropertyName("links")]
        public Links Links { get; set; }
        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; }
        [JsonPropertyName("launchpad")]
        public Launchpad Launchpad { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
