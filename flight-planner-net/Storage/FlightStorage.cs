using flight_planner_net.Models;
using System.Collections.Concurrent;

namespace flight_planner_net.Storage
{
    public static class FlightStorage
    {
        private static ConcurrentBag<Flight> _flights = new ConcurrentBag<Flight>();
        private static int _id = 0;
        private static readonly object _lock = new object();

        public static void AddFlight(Flight flight)
        {
            if (flight == null ||
                flight.From == null ||
                flight.To == null ||
                string.IsNullOrWhiteSpace(flight.From.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.To.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.Carrier) ||
                string.IsNullOrWhiteSpace(flight.DepartureTime) ||
                string.IsNullOrWhiteSpace(flight.ArrivalTime) ||
                flight.From.AirportCode.Trim().Equals(flight.To.AirportCode.Trim(), StringComparison.OrdinalIgnoreCase) ||
                !DateTime.TryParse(flight.DepartureTime, out DateTime departureDate) ||
                !DateTime.TryParse(flight.ArrivalTime, out DateTime arrivalDate) ||
                departureDate >= arrivalDate)
            {
                throw new ArgumentException("Invalid flight details.");
            }

            lock (_lock)
            {
                if (_flights.Any(existingFlight =>
                    existingFlight.From.AirportCode.Equals(flight.From.AirportCode, StringComparison.OrdinalIgnoreCase) &&
                    existingFlight.To.AirportCode.Equals(flight.To.AirportCode, StringComparison.OrdinalIgnoreCase) &&
                    existingFlight.DepartureTime == flight.DepartureTime &&
                    existingFlight.ArrivalTime == flight.ArrivalTime))
                {
                    throw new InvalidOperationException("Duplicate flight.");
                }

                flight.Id = ++_id;
                _flights.Add(flight);
            }
        }

        public static void ClearFlights()
        {
            _flights = new ConcurrentBag<Flight>();
        }

        public static IEnumerable<Flight> GetAllFlights()
        {
            return _flights.ToList();
        }

        public static void DeleteFlight(int id)
        {
            var updatedFlights = new ConcurrentBag<Flight>();
            bool flightFound = false;

            foreach (var flight in _flights)
            {
                if (flight.Id == id)
                {
                    flightFound = true;
                }
                else
                {
                    updatedFlights.Add(flight);
                }
            }

            if (flightFound)
            {
                _flights = updatedFlights;
            }
        }

        public static IEnumerable<Airport> SearchAirports(string phrase)
        {
            phrase = phrase.Trim().ToLower();

            return _flights
                .SelectMany(flight => new[] { flight.From, flight.To })
                .Where(airport =>
                    airport.AirportCode.ToLower().Contains(phrase) ||
                    airport.City.ToLower().Contains(phrase) ||
                    airport.Country.ToLower().Contains(phrase))
                .Distinct()
                .ToList();
        }

        public static IEnumerable<Flight> SearchFlights(SearchFlightsRequest request)
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

            return _flights.Where(f =>
                f.From.AirportCode.Equals(request.From, StringComparison.OrdinalIgnoreCase) &&
                f.To.AirportCode.Equals(request.To, StringComparison.OrdinalIgnoreCase) &&
                DateTime.TryParse(f.DepartureTime, out DateTime flightDeparture) &&
                flightDeparture.Date == departureDate.Date);
        }
    }
}
