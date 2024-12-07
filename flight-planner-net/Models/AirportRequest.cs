namespace flight_planner_net.Models
{
    public record AirportRequest
    {
        public required string Country { get; set; }

        public required string City { get; set; }

        public required string Airport { get; set; }
    }
}