using FlightPlanner.Core;
using FlightPlanner.Core.Services;
using FluentValidation;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Get
{
    public class SearchFlightsCommandHandler : IRequestHandler<SearchFlightsCommand, Result>
    {
        private readonly IFlightService _flightService;
        private readonly IValidator<SearchFlightsCommand> _validator;

        public SearchFlightsCommandHandler(
            IFlightService flightService,
            IValidator<SearchFlightsCommand> validator)
        {
            _flightService = flightService;
            _validator = validator;
        }

        public Task<Result> Handle(SearchFlightsCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request); 
            if (!validationResult.IsValid)
            {
                return Task.FromResult(new Result
                {
                    Status = ResultStatus.BadRequest,
                });
            }

            var flights = _flightService.SearchFlights(request.From, request.To, request.DepartureTime);

            var response = new 
            {
                page = 0, 
                totalItems = flights.Count(),
                items = flights
            };

            return Task.FromResult(new Result
            {
                Status = ResultStatus.Success,
                Response = response
            });
        }
    }
}