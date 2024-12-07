using FlightPlanner.Core.Model;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class DbClearingService : DBService, IDbClearingService
    {
        public DbClearingService(FlightPlannerDbContext context) : base(context)
        {
        }

        public ServiceResult Clear<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();

            return new ServiceResult(true);
        }
    }
}