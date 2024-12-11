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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> FindFlightById(int id)
        {
            var result = await _mediator.Send(new GetFlightCommand { Id = id });
            return this.ToActionResult(result);
        }
        
        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> SearchAirports(string search)
        {
            var result = await _mediator.Send(new GetAirportsCommand { SearchTerm = search });
            return this.ToActionResult(result);
        }
        
        [HttpPost]
        [Route("flights/search")]
        public async Task<IActionResult> SearchFlights(SearchFlightsRequest request)
        {
            var result = await _mediator.Send(new SearchFlightsCommand
            {
                From = request.From,
                To = request.To,
                DepartureTime = Convert.ToDateTime(request.DepartureTime)
            });
            return this.ToActionResult(result);
        }
    }
}