using BookingService.Application.DTOs.Response.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetByFilter;

public class GetTripsByFilterQuery : IRequest<IEnumerable<TripResponseDto>>
{
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public Guid? TypeId { get; set; }
}