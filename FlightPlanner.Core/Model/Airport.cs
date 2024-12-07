using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Model
{
    public class Airport : Entity
    {
        [MaxLength(100)] public string Country { get; set; }
        [MaxLength(200)] public string City { get; set; }
        [MaxLength(20)] public string AirportCode { get; set; }
    }
}