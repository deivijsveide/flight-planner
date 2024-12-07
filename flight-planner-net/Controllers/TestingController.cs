using flight_planner_net.Storage;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightStorage _storage;
        public TestingController(FlightStorage storage)
        {
            _storage = storage;
        }
        
        [HttpPost]
        [Route("Clear")]
        public IActionResult Clear()
        {
            _storage.ClearFlights();
            return Ok();
        }
    }
}