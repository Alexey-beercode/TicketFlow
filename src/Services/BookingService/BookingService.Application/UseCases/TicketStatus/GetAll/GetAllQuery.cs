using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.DTOs.Response.TicketStatus;
using MediatR;

namespace BookingService.Application.UseCases.TicketStatus.GetAll;

public class GetAllQuery:IRequest<IEnumerable<TicketStatusResponseDto>>, IRequest<IEnumerable<TicketResponseDto>>
{
    
}