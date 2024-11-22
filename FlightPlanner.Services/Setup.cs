using System.Reflection;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlightPlanner.Services
{
    public static class Setup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDbService, DBService>();
            services.AddScoped<IDbClearingService, DbClearingService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IAirportService, AirportService>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(executingAssembly);
            services.AddAutoMapper(executingAssembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(executingAssembly));
        }
    }
}