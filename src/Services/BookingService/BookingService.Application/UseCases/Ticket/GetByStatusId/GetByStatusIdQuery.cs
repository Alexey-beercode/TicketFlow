using BookingService.Application.DTOs.Response.Ticket;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetByStatusId;

public class GetByStatusIdQuery:IRequest<IEnumerable<TicketResponseDto>>
{
    public Guid StatusId { get; set; }
}