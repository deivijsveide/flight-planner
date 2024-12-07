using AutoMapper;
using flight_planner_net.Models;
using FlightPlanner.Core.Model;
using FlightPlanner.Services.Features.Airports;
using FlightPlanner.Services.Features.Airports.Models;
using FlightPlanner.Services.Features.Flights.Models;
using FlightPlanner.Services.Features.Flights.UseCases.Add;

namespace flight_planner_net.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FlightRequest, AddFlightCommand>();
            CreateMap<AirportRequest, AddAirportCommand>()
                .ForMember(airport => airport.Airport,
                    options => options.MapFrom(request => request.Airport));
            CreateMap<AddFlightCommand, Flight>();
            CreateMap<AddAirportCommand, Airport>()
                .ForMember(airport => airport.AirportCode, options =>
                    options.MapFrom(command => command.Airport));
            CreateMap<Flight, FlightViewModel>();
            CreateMap<Airport, AirportViewModel>()
                .ForMember(response => response.Airport,
                    options => options.MapFrom(airport => airport.AirportCode));
        }
    }
}