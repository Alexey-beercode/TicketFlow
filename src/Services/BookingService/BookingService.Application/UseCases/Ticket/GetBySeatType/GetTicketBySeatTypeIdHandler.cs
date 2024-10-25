using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.Exceptions;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetBySeatType;

public class GetTicketBySeatTypeIdHandler:IRequestHandler<GetTicketBySeatTypeIdQuery,IEnumerable<TicketResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketFacade _ticketFacade;

    public GetTicketBySeatTypeIdHandler(IUnitOfWork unitOfWork, ITicketFacade ticketFacade)
    {
        _unitOfWork = unitOfWork;
        _ticketFacade = ticketFacade;
    }

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetTicketBySeatTypeIdQuery request, CancellationToken cancellationToken)
    {
        var seatType = await _unitOfWork.SeatTypes.GetByIdAsync(request.SeatTypeId, cancellationToken);

        if (seatType is null)
        {
            throw new EntityNotFoundException("Seat type", request.SeatTypeId);
        }

        var tickets = await _unitOfWork.Tickets.GetBySeatTypeIdAsync(seatType.Id, cancellationToken);

        return await _ticketFacade.GetListOfFullTicketInfoAsync(tickets, cancellationToken);
    }
}