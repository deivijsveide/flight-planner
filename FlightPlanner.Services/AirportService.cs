using FlightPlanner.Core.Model;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : IAirportService
    {
        private readonly FlightPlannerDbContext _context;

        public AirportService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Airport> SearchAirports(string searchTerm)
        {
            var normalizedSearchTerm = searchTerm.Trim().ToLower();

            return _context.Airports
                .Where(a => a.City.ToLower().Contains(normalizedSearchTerm) ||
                            a.Country.ToLower().Contains(normalizedSearchTerm) ||
                            a.AirportCode.ToLower().Contains(normalizedSearchTerm))
                .ToList();
        }
    }
}