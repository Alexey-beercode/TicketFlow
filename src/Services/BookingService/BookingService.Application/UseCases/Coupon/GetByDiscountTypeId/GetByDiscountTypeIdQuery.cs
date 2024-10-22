using BookingService.Application.DTOs.Response.Coupon;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetByDiscountTypeId;

public class GetByDiscountTypeIdQuery:IRequest<IEnumerable<CouponResponseDto>>
{
    public Guid DiscountTypeId { get; set; }
}