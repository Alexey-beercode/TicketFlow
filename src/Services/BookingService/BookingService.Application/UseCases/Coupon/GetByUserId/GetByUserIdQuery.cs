using BookingService.Application.DTOs.Response.Coupon;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetByUserId;

public class GetByUserIdQuery:IRequest<IEnumerable<CouponResponseDto>>
{
    public Guid UserId { get; set; }
}