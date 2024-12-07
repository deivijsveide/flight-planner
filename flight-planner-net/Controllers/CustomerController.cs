using flight_planner_net.Models;
using flight_planner_net.Storage;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightStorage _storage;
        public CustomerController(FlightStorage storage)
        {
            _storage = storage;
        }
        
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightByid(int id)
        {
            var flight = _storage.GetAllFlights().FirstOrDefault(f => f.Id == id);
            return flight != null ? Ok(flight) : NotFound();
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports([FromQuery] string search)
        {
            var airports = _storage.SearchAirports(search);
            return Ok(airports);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights([FromBody] SearchFlightsRequest request)
        {
            try
            {
                var flights = _storage.SearchFlights(request);
                var pageResult = new PageResult<Flight>
                {
                    Page = 0,
                    TotalItems = flights.Count(),
                    Items = flights.ToList()
                };
                return Ok(pageResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}