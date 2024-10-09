namespace BookingService.Domain.Entities;

public class TripSeatAvailability
{
    public int SeatsAvailable { get; set; }
    public Guid TripId { get; set; }
    public Guid SeatTypeId { get; set; }
}