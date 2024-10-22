using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.DTOs.Response.TicketStatus;
using MediatR;

namespace BookingService.Application.UseCases.TicketStatus.GetById;

public class GetByIdQuery:IRequest<TicketStatusResponseDto>
{
    public Guid Id { get; set; }
}