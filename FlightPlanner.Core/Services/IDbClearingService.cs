using FlightPlanner.Core.Model;

namespace FlightPlanner.Core.Services
{
    public interface IDbClearingService : IDbService
    {
        ServiceResult Clear<T>() where T : Entity;
    }
}