using flight_planner_net.Models;
using flight_planner_net.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetAllFlights().FirstOrDefault(f => f.Id == id);
            return flight != null ? Ok(flight) : NotFound();
        }

        [HttpPost]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            try
            {
                FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}