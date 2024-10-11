using BookingService.Application.DTOs.Response.Coupon;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetActiveCoupons;

public class GetActiveCouponsQuery:IRequest<IEnumerable<CouponResponseDto>>
{
}