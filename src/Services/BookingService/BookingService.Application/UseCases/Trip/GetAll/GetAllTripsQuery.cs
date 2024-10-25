using BookingService.Application.DTOs.Response.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetAll;

public class GetAllTripsQuery:IRequest<IEnumerable<TripResponseDto>>
{
    
}