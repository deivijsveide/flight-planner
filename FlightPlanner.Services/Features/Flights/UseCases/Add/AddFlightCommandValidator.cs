using FluentValidation;

namespace FlightPlanner.Services.Features.Flights.UseCases.Add
{
    public class AddFlightCommandValidator : AbstractValidator<AddFlightCommand>
    {
        public AddFlightCommandValidator()
        {

            RuleFor(flight => flight.ArrivalTime).NotEmpty();
            RuleFor(flight => flight.DepartureTime).NotEmpty();
            RuleFor(flight => flight.DepartureTime).Must((flight, departureTime) =>
                    DateTime.Parse(departureTime) < DateTime.Parse(flight.ArrivalTime))
                .When(flight =>
                    !string.IsNullOrEmpty(flight.ArrivalTime) && !string.IsNullOrEmpty(flight.DepartureTime));
            RuleFor(flight => flight.Carrier).NotEmpty();

            RuleFor(flight => flight.From).NotNull();
            RuleFor(flight => flight.To).NotNull();
            RuleFor(flight => flight.From.Airport).NotEmpty();
            RuleFor(flight => flight.To.Airport).NotEmpty();

            RuleFor(flight => flight.From.Airport).Must((flight, airport) =>
                    airport.ToLower().Trim() != flight.To.Airport.ToLower().Trim())
                .When(flight =>
                    !string.IsNullOrEmpty(flight.From?.Airport) && !string.IsNullOrEmpty(flight.To?.Airport));
            RuleFor(flight => flight.From.City).NotEmpty();
            RuleFor(flight => flight.From.Country).NotEmpty();
            RuleFor(flight => flight.To.Airport).NotEmpty();
            RuleFor(flight => flight.To.City).NotEmpty();
            RuleFor(flight => flight.To.Country).NotEmpty();
        }
    }
}
