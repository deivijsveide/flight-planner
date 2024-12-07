using FlightPlanner.Core.Model;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController(IDbClearingService dbClearingService) : ControllerBase
    {
<<<<<<< HEAD
        private readonly IDbClearingService _dbClearingService = dbClearingService;
=======
        private readonly FlightStorage _storage;
        public TestingController(FlightStorage storage)
        {
            _storage = storage;
        }
>>>>>>> main
        
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
<<<<<<< HEAD
            _dbClearingService.Clear<Airport>();
            _dbClearingService.Clear<Flight>();
            
=======
            _storage.ClearFlights();
>>>>>>> main
            return Ok();
        }
    }
}