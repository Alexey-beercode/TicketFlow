using AutoMapper;
using BookingService.Application.DTOs.Request.TripSeatAvailability;
using BookingService.Application.DTOs.Response.TripSeatAvailability;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class TripSeatAvailabilityProfile:Profile
{
    public TripSeatAvailabilityProfile()
    {
        CreateMap<TripSeatAvailability, TripSeatAvailabilityDto>();
        CreateMap<TripSeatAvailabilityDto, TripSeatAvailability>();
        CreateMap<TripSeatAvailabilityRequestDto, TripSeatAvailability>();
    }
}