using AutoMapper;
using flight_planner_net.Extensions;
using flight_planner_net.Models;
using FlightPlanner.Services.Features.Flights.UseCases.Add;
using FlightPlanner.Services.Features.Flights.UseCases.Delete;
using FlightPlanner.Services.Features.Flights.UseCases.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly FlightStorage _storage;
        public AdminController(FlightStorage storage)
        {
            _storage = storage;
        }
        
        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            var flight = _storage.GetAllFlights().FirstOrDefault(f => f.Id == id);
            return flight != null ? Ok(flight) : NotFound();
        }

        [HttpPost]
        [Route("flights")]
        public async Task<IActionResult> AddFlight(FlightRequest request)
        {
            try
            {
                _storage.AddFlight(flight);
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
        public async Task<IActionResult> DeleteFlight(int id)
        {
            _storage.DeleteFlight(id);
            return Ok();
        }
    }
}