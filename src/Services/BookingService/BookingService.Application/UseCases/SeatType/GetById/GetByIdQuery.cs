using BookingService.Application.DTOs.Response.SeatType;
using MediatR;

namespace BookingService.Application.UseCases.SeatType.GetById;

public class GetByIdQuery:IRequest<SeatTypeResponseDto>
{
    public Guid Id { get; set; }
}