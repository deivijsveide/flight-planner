using AutoMapper;
using flight_planner_net.Extensions;
using FlightPlanner.Core.Model;
using FlightPlanner.Services.Features.Airports.Get;
using FlightPlanner.Services.Features.Flights.UseCases.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
<<<<<<< HEAD
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
=======
        private readonly FlightStorage _storage;
        public CustomerController(FlightStorage storage)
        {
            _storage = storage;
>>>>>>> main
        }
        
        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> FindFlightById(int id)
        {
<<<<<<< HEAD
            var result = await _mediator.Send(new GetFlightCommand { Id = id });
            return this.ToActionResult(result);
=======
            var flight = _storage.GetAllFlights().FirstOrDefault(f => f.Id == id);
            return flight != null ? Ok(flight) : NotFound();
>>>>>>> main
        }
        
        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> SearchAirports(string search)
        {
<<<<<<< HEAD
            var result = await _mediator.Send(new GetAirportsCommand { SearchTerm = search });
            return this.ToActionResult(result);
=======
            var airports = _storage.SearchAirports(search);
            return Ok(airports);
>>>>>>> main
        }
        
        [HttpPost]
        [Route("flights/search")]
        public async Task<IActionResult> SearchFlights(SearchFlightsRequest request)
        {
            var result = await _mediator.Send(new SearchFlightsCommand
            {
<<<<<<< HEAD
                From = request.From,
                To = request.To,
                DepartureTime = Convert.ToDateTime(request.DepartureTime)
            });
            return this.ToActionResult(result);
=======
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
>>>>>>> main
        }
    }
}