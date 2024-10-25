using BookingService.Application.DTOs.Response.Ticket;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetById;

public class GetByIdQuery:IRequest<TicketResponseDto>
{
    public Guid Id { get; set; }
}