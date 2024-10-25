using AutoMapper;
using BookingService.Application.DTOs.Response.DiscountType;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class DiscountTypeProfile:Profile
{
    public DiscountTypeProfile()
    {
        CreateMap<DiscountType, DiscountTypeResponseDto>();
    }
}