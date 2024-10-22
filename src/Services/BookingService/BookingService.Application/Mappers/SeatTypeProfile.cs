using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class SeatTypeProfile : Profile
{
    public SeatTypeProfile()
    {
        CreateMap<SeatType, SeatTypeResponseDto>();
    }
}