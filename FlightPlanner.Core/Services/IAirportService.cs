using FlightPlanner.Core.Model;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService
    {
        IEnumerable<Airport> SearchAirports(string searchTerm);
    }
}