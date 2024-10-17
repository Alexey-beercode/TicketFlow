using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.DTOs.Response.TripType;
using MediatR;

namespace BookingService.Application.UseCases.TripType.GetById;

public class GetByIdQuery:IRequest<TripTypeResponseDto>
{
    public Guid Id { get; set; }
}