using FlightPlanner.Core;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Delete
{
    public class DeleteFlightCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}