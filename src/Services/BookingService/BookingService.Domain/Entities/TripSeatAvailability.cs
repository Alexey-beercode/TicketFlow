using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class TripSeatAvailability:BaseEntity
{
    public int SeatsAvailable { get; set; }
    public Guid TripId { get; set; }
    public Guid SeatTypeId { get; set; }
}