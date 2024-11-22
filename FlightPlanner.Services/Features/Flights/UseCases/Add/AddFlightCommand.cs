using FlightPlanner.Core;
using FlightPlanner.Services.Features.Airports;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Add
{
    public class AddFlightCommand : IRequest<Result>
    {
        public AddAirportCommand From { get; set; }

        public AddAirportCommand To { get; set; }

        public string? Carrier { get; set; }

        public string? DepartureTime { get; set; }

        public string? ArrivalTime { get; set; }
    }
}