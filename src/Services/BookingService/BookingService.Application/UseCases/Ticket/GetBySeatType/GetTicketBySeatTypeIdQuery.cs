using BookingService.Application.DTOs.Response.Ticket;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetBySeatType;

public class GetTicketBySeatTypeIdQuery:IRequest<IEnumerable<TicketResponseDto>>
{
    public Guid SeatTypeId { get; set; }
}