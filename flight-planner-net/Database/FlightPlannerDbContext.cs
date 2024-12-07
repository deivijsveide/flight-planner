using flight_planner_net.Models;
using Microsoft.EntityFrameworkCore;

namespace flight_planner_net.Database
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}