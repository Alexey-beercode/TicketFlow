namespace BookingService.Application.DTOs.Request.TripSeatAvailability;

public class TripSeatAvailabilityRequestDto
{
    public int SeatsAvailable { get; set; }
    public Guid SeatTypeId { get; set; }
}