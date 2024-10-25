using BookingService.Application.DTOs.Response.Ticket;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetAll;

public class GetAllTicketsQuery:IRequest<IEnumerable<TicketResponseDto>>
{
    
}