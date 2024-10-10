namespace BookingService.Application.DTOs.Response.Coupon;

public class CouponResponseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public decimal DiscountValue { get; set; }
    public bool IsPersonalized { get; set; }
    public int MaxUses { get; set; }
    public int UsedCount { get; set; }
    public DateTime ValidUntil { get; set; }
    public Domain.Entities.DiscountType DiscountType { get; set; }
}