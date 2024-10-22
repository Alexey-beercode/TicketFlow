using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class Coupon:BaseEntity
{
    public string Code { get; set; }
    public decimal DiscountValue { get; set; }
    public bool IsPersonalized { get; set; }
    public int MaxUses { get; set; }
    public int UsedCount { get; set; }
    public DateTime ValidUntil { get; set; }
    public Guid DiscountTypeId { get; set; }
}