using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class Ticket:BaseEntity
{
    public decimal Price { get; set; }
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public Guid StatusId { get; set; }
    public Guid? CouponId { get; set; }
    public Guid SeatTypeId { get; set; }
}