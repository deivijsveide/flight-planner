namespace FlightPlanner.Core.Model
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string? DepartureTime { get; set; }
    }
}