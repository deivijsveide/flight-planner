namespace FlightPlanner.Core
{
    public class Result
    {
        public object Response { get; set; }

        public ResultStatus Status { get; set; }
    }

    public enum ResultStatus
    {
        Success = 0,
        NotFound = 1,
        BadRequest = 2,
        Conflict = 3,
        Created = 4,
        NoContent = 5,
    }
}