using BookingService.Application.DTOs.Response.SeatType;
using MediatR;

namespace BookingService.Application.UseCases.SeatType.GetAll;

public class GetAllQuery:IRequest<IEnumerable<SeatTypeResponseDto>>
{
    
}