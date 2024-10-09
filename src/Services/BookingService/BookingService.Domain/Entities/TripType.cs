using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class TripType:BaseEntity
{
    public string Name { get; set; }
}