using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.UseCases.Coupon.Create;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<CreateCouponCommand, Coupon>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<Coupon, CouponResponseDto>();
    }
}