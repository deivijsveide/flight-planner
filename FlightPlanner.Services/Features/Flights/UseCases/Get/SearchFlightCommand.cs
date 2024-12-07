using FlightPlanner.Core;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Get
{
    public class SearchFlightsCommand : IRequest<Result>
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}