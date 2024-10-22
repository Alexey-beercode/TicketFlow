using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class Trip:BaseEntity
{
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal BasePrice { get; set; }
    public Guid TypeId { get; set; }
}