using flight_planner_net.Database;
using flight_planner_net.Models;
using Microsoft.EntityFrameworkCore;

namespace flight_planner_net.Storage
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object _lock = new object();

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public void AddFlight(Flight flight)
        {
            if (flight == null ||
                flight.From == null ||
                flight.To == null ||
                string.IsNullOrWhiteSpace(flight.From.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.To.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.Carrier) ||
                string.IsNullOrWhiteSpace(flight.DepartureTime) ||
                string.IsNullOrWhiteSpace(flight.ArrivalTime) ||
                flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower() ||
                !DateTime.TryParse(flight.DepartureTime, out DateTime departureDate) ||
                !DateTime.TryParse(flight.ArrivalTime, out DateTime arrivalDate) ||
                departureDate >= arrivalDate)
            {
                throw new ArgumentException("Invalid flight details.");
            }

            lock (_lock)
            {
                var duplicate = _context.Flights.Any(existingFlight =>
                    existingFlight.From.AirportCode.Trim().ToLower() == flight.From.AirportCode.Trim().ToLower() &&
                    existingFlight.To.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower() &&
                    existingFlight.DepartureTime == flight.DepartureTime &&
                    existingFlight.ArrivalTime == flight.ArrivalTime);

                if (duplicate)
                {
                    throw new InvalidOperationException("Duplicate flight.");
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();
            }
        }

        public void ClearFlights()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();
        }

        public void DeleteFlight(int id)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Airport> SearchAirports(string phrase)
        {
            phrase = phrase.Trim().ToLower();

            var airports = _context.Airports.ToList();

            return airports
                .Where(airport =>
                    airport.AirportCode.ToLower().Contains(phrase) ||
                    airport.City.ToLower().Contains(phrase) ||
                    airport.Country.ToLower().Contains(phrase))
                .Distinct()
                .ToList();
        }

        public IEnumerable<Flight> SearchFlights(SearchFlightsRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.From) ||
                string.IsNullOrWhiteSpace(request.To) ||
                string.IsNullOrWhiteSpace(request.DepartureDate))
            {
                throw new ArgumentException("Invalid search parameters.");
            }

            if (request.From.Trim().Equals(request.To.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("From and To airports cannot be the same.");
            }

            if (!DateTime.TryParse(request.DepartureDate, out DateTime departureDate))
            {
                throw new ArgumentException("Invalid departure date.");
            }

            var flights = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            return flights
                .Where(f =>
                    f.From.AirportCode.Equals(request.From, StringComparison.OrdinalIgnoreCase) &&
                    f.To.AirportCode.Equals(request.To, StringComparison.OrdinalIgnoreCase) &&
                    DateTime.Parse(f.DepartureTime).Date == departureDate.Date)
                .ToList();
        }
    }
}
