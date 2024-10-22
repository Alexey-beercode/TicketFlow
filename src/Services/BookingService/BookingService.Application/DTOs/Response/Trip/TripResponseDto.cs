using BookingService.Application.DTOs.BaseDtos;
using BookingService.Application.DTOs.Response.TripSeatAvailability;
using BookingService.Application.DTOs.Response.TripType;
using BookingService.Domain.Entities;

namespace BookingService.Application.DTOs.Response.Trip;

public class TripResponseDto:BaseDto
{
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal BasePrice { get; set; }
    public TripTypeResponseDto Type { get; set; }
    public List<TripSeatAvailabilityDto> SeatsAvailability { get; set; }
}