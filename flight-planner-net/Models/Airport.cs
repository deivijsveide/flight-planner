using System.Text.Json.Serialization;

namespace flight_planner_net.Models
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}