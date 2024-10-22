using MediatR;

namespace BookingService.Application.UseCases.Coupon.Create;

public class CreateCouponCommand:IRequest
{
    public string Code { get; set; }
    public decimal DiscountValue { get; set; }
    public bool IsPersonalized { get; set; }
    public int MaxUses { get; set; }
    public DateTime ValidUntil { get; set; }
    public Guid DiscountTypeId { get; set; }
    public Guid UserId { get; set; }
}