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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AdminController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            var result = await _mediator.Send(new GetFlightCommand { Id = id });
            return this.ToActionResult(result);
        }

        [HttpPost]
        [Route("flights")]
        public async Task<IActionResult> AddFlight(FlightRequest request)
        {
            return this.ToActionResult(
                await _mediator.Send(
                    _mapper.Map<AddFlightCommand>(request)));
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var result = await _mediator.Send(new DeleteFlightCommand { Id = id });
            return this.ToActionResult(result);
        }
    }
}