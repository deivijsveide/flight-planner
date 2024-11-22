namespace flight_planner_net.Models
{
    public record FlightRequest
    {
        public required AirportRequest From { get; set; }

        public required AirportRequest To { get; set; }

        public required string Carrier { get; set; }

        public required string DepartureTime { get; set; }

        public required string ArrivalTime { get; set; }
    }
}