using BookingService.Application.DTOs.Response.Coupon;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetAll;

public class GetAllCouponsQuery:IRequest<IEnumerable<CouponResponseDto>>
{
    
}