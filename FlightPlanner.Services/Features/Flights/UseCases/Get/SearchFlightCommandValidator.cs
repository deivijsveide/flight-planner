using FluentValidation;

namespace FlightPlanner.Services.Features.Flights.UseCases.Get
{
    public class SearchFlightsCommandValidator : AbstractValidator<SearchFlightsCommand>
    {
        public SearchFlightsCommandValidator()
        {
            RuleFor(flight => flight.From)
                .NotEmpty();
            RuleFor(flight => flight.To)
                .NotEmpty();
            RuleFor(flight => new { flight.From, flight.To })
                .Must(flightProps => flightProps.From != flightProps.To);
        }
    }
}