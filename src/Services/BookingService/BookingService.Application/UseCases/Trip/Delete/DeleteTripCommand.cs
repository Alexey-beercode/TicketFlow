using MediatR;

namespace BookingService.Application.UseCases.Trip.Delete;

public class DeleteTripCommand:IRequest
{
    public Guid Id { get; set; }
}