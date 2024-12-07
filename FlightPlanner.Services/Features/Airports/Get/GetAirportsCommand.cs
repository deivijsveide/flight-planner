using FlightPlanner.Core;
using MediatR;

namespace FlightPlanner.Services.Features.Airports.Get
{
    public class GetAirportsCommand : IRequest<Result>
    {
        public string SearchTerm { get; set; }
    }
}