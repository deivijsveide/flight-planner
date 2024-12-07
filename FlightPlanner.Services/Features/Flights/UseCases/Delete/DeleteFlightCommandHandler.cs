using FlightPlanner.Core.Services;
using FlightPlanner.Core;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Delete
{
    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, Result>
    {
        private readonly IFlightService _flightService;

        public DeleteFlightCommandHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public Task<Result> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetById(request.Id);
        
            if (flight == null)
            {
                return Task.FromResult(new Result
                {
                    Status = ResultStatus.Success,
                    Response = "Flight not found"
                });
            }

            _flightService.Delete(request.Id);

            return Task.FromResult(new Result
            {
                Status = ResultStatus.Success,
                Response = "Flight successfully deleted"
            });
        }
    }
}