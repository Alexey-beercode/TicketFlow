using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.DTOs.Response.TripType;
using MediatR;

namespace BookingService.Application.UseCases.TripType.GetAll;

public class GetAllQuery:IRequest<IEnumerable<TripTypeResponseDto>>
{
    
}