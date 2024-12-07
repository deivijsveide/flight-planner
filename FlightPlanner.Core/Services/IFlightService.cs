using FlightPlanner.Core.Model;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService
    {
        void Create(Flight flight);

        Flight GetById(int id);

        void Delete(int id);

        IEnumerable<Flight> GetAll();

        IEnumerable<Flight> SearchFlights(string from, string to, DateTime departureDate);
        
        bool IsDuplicate(Flight flight);
    }
}