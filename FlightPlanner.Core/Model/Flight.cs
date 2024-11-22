using System.ComponentModel.DataAnnotations;

namespace FlightPlanner.Core.Model
{
    public class Flight : Entity
    {
        public Airport? From { get; set; }
        public Airport? To { get; set; }
        [MaxLength(100)] public string Carrier { get; set; }
        [MaxLength(50)] public string DepartureTime { get; set; }
        [MaxLength(50)] public string ArrivalTime { get; set; }
    }
}