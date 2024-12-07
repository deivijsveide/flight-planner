using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace flight_planner_net.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [JsonPropertyName("airport")]
        [MaxLength(20)]
        public string AirportCode { get; set; }
    }
}