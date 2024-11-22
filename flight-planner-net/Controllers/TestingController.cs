using FlightPlanner.Core.Model;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController(IDbClearingService dbClearingService) : ControllerBase
    {
        private readonly IDbClearingService _dbClearingService = dbClearingService;
        
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _dbClearingService.Clear<Airport>();
            _dbClearingService.Clear<Flight>();
            
            return Ok();
        }
    }
}