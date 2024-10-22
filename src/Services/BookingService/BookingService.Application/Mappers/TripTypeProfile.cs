using AutoMapper;
using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.DTOs.Response.TripType;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class TripTypeProfile:Profile
{
    public TripTypeProfile()
    {
        CreateMap<TripType, TripTypeResponseDto>();
    }
}