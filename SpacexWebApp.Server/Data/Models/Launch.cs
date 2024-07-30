using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpacexWebApp.Server.Data.Models
{
    public class Patch
    {
        [JsonPropertyName("small")]
        public string Small { get; set; }

        [JsonPropertyName("large")]
        public string Large { get; set; }
    }

    public class Reddit
    {
        [JsonPropertyName("campaign")]
        public string Campaign { get; set; }

        [JsonPropertyName("launch")]
        public string Launch { get; set; }

        [JsonPropertyName("media")]
        public string Media { get; set; }

        [JsonPropertyName("recovery")]
        public string Recovery { get; set; }
    }

    public class Flickr
    {
        [JsonPropertyName("small")]
        public List<string> Small { get; set; }

        [JsonPropertyName("original")]
        public List<string> Original { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("patch")]
        public Patch Patch { get; set; }

        [JsonPropertyName("reddit")]
        public Reddit Reddit { get; set; }

        [JsonPropertyName("flickr")]
        public Flickr Flickr { get; set; }

        [JsonPropertyName("presskit")]
        public string Presskit { get; set; }

        [JsonPropertyName("webcast")]
        public string Webcast { get; set; }

        [JsonPropertyName("youtube_id")]
        public string YoutubeId { get; set; }

        [JsonPropertyName("article")]
        public string Article { get; set; }

        [JsonPropertyName("wikipedia")]
        public string Wikipedia { get; set; }
    }

    public class CrewMember
    {
        [JsonPropertyName("crew")]
        public string Id { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }

    public class Launch
    {
        [JsonPropertyName("flight_number")]
        public int FlightNumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("date_utc")]
        public string DateUtc { get; set; }

        [JsonPropertyName("rocket")]
        public string Rocket { get; set; }

        [JsonPropertyName("success")]
        public bool? Success { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("links")]
        public Links Links { get; set; }

        [JsonPropertyName("crew")]
        public List<CrewMember> Crew { get; set; }

        [JsonPropertyName("launchpad")]
        public string Launchpad { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}