using BookingService.Application.DTOs.Response.Ticket;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetByUserId;

public class GetByUserIdQuery:IRequest<IEnumerable<TicketResponseDto>>
{
    public Guid UserId { get; set; }
}