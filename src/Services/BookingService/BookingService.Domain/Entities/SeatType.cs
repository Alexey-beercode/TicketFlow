using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class SeatType:BaseEntity
{
    public string Name { get; set; }
    public decimal PriceMultiplier { get; set; }
}