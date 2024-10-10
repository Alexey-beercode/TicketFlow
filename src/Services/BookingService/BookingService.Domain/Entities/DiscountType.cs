using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class DiscountType:BaseEntity
{
    public string Name { get; set; }
}