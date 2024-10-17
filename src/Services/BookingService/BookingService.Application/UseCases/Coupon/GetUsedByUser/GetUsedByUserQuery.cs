using BookingService.Application.DTOs.Response.Coupon;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetUsedByUser;

public class GetUsedByUserQuery:IRequest<IEnumerable<CouponResponseDto>>
{
    public Guid UserId { get; set; }
}