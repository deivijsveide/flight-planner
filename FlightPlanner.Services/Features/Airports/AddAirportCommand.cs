namespace FlightPlanner.Services.Features.Airports
{
    public class AddAirportCommand
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Airport { get; set; }
    }
}