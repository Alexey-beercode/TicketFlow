using BookingService.Application.DTOs.Request.TripSeatAvailability;
using MediatR;

namespace BookingService.Application.UseCases.Trip.Create;

public class CreateTripCommand:IRequest
{
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public decimal BasePrice { get; set; }
    public Guid TypeId { get; set; }
    public List<TripSeatAvailabilityRequestDto> TripSeatAvailabilityRequestDtos { get; set; }
}