using FlightPlanner.Core;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToActionResult(this ControllerBase controller, Result result)
        {
            switch (result.Status)
            {
                case ResultStatus.Success:
                    return new OkObjectResult(result.Response);
                case ResultStatus.Created:
                    return new CreatedResult("", result.Response);
                case ResultStatus.NotFound:
                    return new NotFoundResult();
                case ResultStatus.BadRequest:
                    return new BadRequestObjectResult(result.Response);
                case ResultStatus.Conflict:
                    return new ConflictObjectResult(result.Response);
                case ResultStatus.NoContent:
                    return new NoContentResult();
                default:
                    return new OkObjectResult(null);
            }
        }
    }
}