<<<<<<< HEAD
using System.Reflection;
using FlightPlanner.Data;
using flight_planner_net.Handlers;
using FlightPlanner.Core;
using FlightPlanner.Core.Services;
using FlightPlanner.Services;
using FlightPlanner.Services.Features.Airports.Get;
using FlightPlanner.Services.Features.Flights.UseCases.Add;
using FlightPlanner.Services.Features.Flights.UseCases.Delete;
using FlightPlanner.Services.Features.Flights.UseCases.Get;
using FluentValidation;
using MediatR;
=======
using flight_planner_net.Database;
using flight_planner_net.Handlers;
using flight_planner_net.Storage;
>>>>>>> main
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace flight_planner_net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
<<<<<<< HEAD

            builder.Services.AddDbContext<FlightPlannerDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner")));
            
            builder.Services.AddScoped<IDbService, DBService>();
            builder.Services.AddScoped<IDbClearingService, DbClearingService>();
            builder.Services.AddScoped<IFlightService, FlightService>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            builder.Services.AddAutoMapper(executingAssembly);
            
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(executingAssembly));

            builder.Services.AddScoped<IRequestHandler<AddFlightCommand, Result>, AddFlightCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<GetFlightCommand, Result>, GetFlightCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<SearchFlightsCommand, Result>, SearchFlightsCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<GetAirportsCommand, Result>, GetAirportsCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<DeleteFlightCommand, Result>, DeleteFlightCommandHandler>();

            builder.Services.AddScoped<IValidator<AddFlightCommand>, AddFlightCommandValidator>();
            builder.Services.AddScoped<IValidator<SearchFlightsCommand>, SearchFlightsCommandValidator>();

            builder.Services.AddServices();

=======
            builder.Services.AddDbContext<FlightPlannerDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("flight-planner")));
            builder.Services.AddScoped<FlightStorage>();
            
            
>>>>>>> main
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
