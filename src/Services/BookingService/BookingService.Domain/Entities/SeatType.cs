using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class SeatType:BaseEntity
{
    public string Name { get; set; }
    public double PriceMultiplier { get; set; }
}