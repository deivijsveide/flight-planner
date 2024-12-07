using FlightPlanner.Core;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Get
{
    public class GetFlightCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}