using BookingService.Application.DTOs.Response.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetById;

public class GetTripByIdQuery:IRequest<TripResponseDto>
{
    public Guid Id { get; set; }
}