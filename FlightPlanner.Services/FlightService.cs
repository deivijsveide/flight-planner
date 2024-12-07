using FlightPlanner.Core.Model;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object _lock = new object(); 
        
        public FlightService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public void Create(Flight flight)
        {
            lock (_lock)
            {
                _context.Flights.Add(flight);
                _context.SaveChanges();
            }
        }

        public Flight GetById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public void Delete(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Flight> GetAll()
        {
            return _context.Flights.ToList();
        }
        
        public IEnumerable<Flight> SearchFlights(string from, string to, DateTime departureDate)
        {
            var flights = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            return flights
                .Where(f => f.From.AirportCode.Equals(from, StringComparison.OrdinalIgnoreCase) &&
                            f.To.AirportCode.Equals(to, StringComparison.OrdinalIgnoreCase) &&
                            DateTime.TryParse(f.DepartureTime, out var departureTime) && departureTime.Date == departureDate.Date)
                .ToList(); 
        }
        
        public bool IsDuplicate(Flight flight)
        {
                return _context.Flights.Any(existingFlight =>
                    existingFlight.From.AirportCode.Trim().ToLower() == flight.From.AirportCode.Trim().ToLower() &&
                    existingFlight.To.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower() &&
                    existingFlight.DepartureTime == flight.DepartureTime &&
                    existingFlight.ArrivalTime == flight.ArrivalTime);
        }
    }
}