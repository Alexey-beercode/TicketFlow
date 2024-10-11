using MediatR;

namespace BookingService.Application.UseCases.Coupon.Delete;

public class DeleteCouponCommand:IRequest
{
    public Guid Id { get; set; }
}