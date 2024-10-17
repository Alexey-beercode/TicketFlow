using AutoMapper;
using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.UseCases.Trip.Create;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class TripProfile:Profile
{
    public TripProfile()
    {
        CreateMap<Trip, TripResponseDto>();
        CreateMap<CreateTripCommand, Trip>();
    }
}