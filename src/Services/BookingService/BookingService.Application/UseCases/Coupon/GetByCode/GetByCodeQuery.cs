using BookingService.Application.DTOs.Response.Coupon;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetByCode;

public class GetByCodeQuery:IRequest<CouponResponseDto>
{
    public string Code { get; set; }
}