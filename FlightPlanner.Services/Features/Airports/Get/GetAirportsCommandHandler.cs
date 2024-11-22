using AutoMapper;
using FlightPlanner.Core;
using FlightPlanner.Core.Services;
using FlightPlanner.Services.Features.Airports.Models;
using MediatR;

namespace FlightPlanner.Services.Features.Airports.Get
{
    public class GetAirportsCommandHandler : IRequestHandler<GetAirportsCommand, Result>
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public GetAirportsCommandHandler(IAirportService airportService, IMapper mapper)
        {
            _airportService = airportService;
            _mapper = mapper;
        }

        public Task<Result> Handle(GetAirportsCommand request, CancellationToken cancellationToken)
        {
            var airports = _airportService.SearchAirports(request.SearchTerm);

            if (airports == null || !airports.Any())
            {
                return Task.FromResult(new Result
                {
                    Status = ResultStatus.NotFound,
                });
            }

            var airportViewModels = _mapper.Map<IEnumerable<AirportViewModel>>(airports);

            return Task.FromResult(new Result
            {
                Status = ResultStatus.Success,
                Response = airportViewModels
            });
        }
    }
}