using System.Collections;
using BookingService.Application.DTOs.Response.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetByTypeId;

public class GetTripsByTypeIdQuery:IRequest<IEnumerable<TripResponseDto>>
{
    public Guid TypeId { get; set; }
}