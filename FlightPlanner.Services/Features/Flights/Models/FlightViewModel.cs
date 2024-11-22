using FlightPlanner.Services.Features.Airports.Models;

namespace FlightPlanner.Services.Features.Flights.Models
{
    public class FlightViewModel
    {
        public int Id { get; set; }

        public AirportViewModel From { get; set; }

        public AirportViewModel To { get; set; }

        public string Carrier { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }
    }
}