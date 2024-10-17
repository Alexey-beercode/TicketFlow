using MediatR;

namespace BookingService.Application.UseCases.Ticket.Delete;

public class DeleteTicketCommand:IRequest
{
    public Guid TicketId { get; set; }
}