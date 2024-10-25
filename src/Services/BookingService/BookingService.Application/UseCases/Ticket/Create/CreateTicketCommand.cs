using MediatR;

namespace BookingService.Application.UseCases.Ticket.Create;

public class CreateTicketCommand:IRequest
{
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public string? CouponCode { get; set; }
    public Guid SeatTypeId { get; set; }
}