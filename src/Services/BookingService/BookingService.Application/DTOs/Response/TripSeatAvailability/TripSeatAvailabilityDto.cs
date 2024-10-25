using BookingService.Application.DTOs.Response.SeatType;

namespace BookingService.Application.DTOs.Response.TripSeatAvailability;

public class TripSeatAvailabilityDto
{
    public int SeatsAvailable { get; set; }
    public SeatTypeResponseDto SeatType { get; set; }
}