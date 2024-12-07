using System.ComponentModel.DataAnnotations;

namespace flight_planner_net.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        [MaxLength(100)]
        public string Carrier { get; set; }
        [MaxLength(50)]
        public string DepartureTime { get; set; }
        [MaxLength(50)]
        public string ArrivalTime { get; set; }
    }
}