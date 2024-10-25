using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetAll;

public class GetAllTicketHandler:IRequestHandler<GetAllTicketsQuery,IEnumerable<TicketResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketFacade _ticketFacade;

    public GetAllTicketHandler(IUnitOfWork unitOfWork, ITicketFacade ticketFacade)
    {
        _unitOfWork = unitOfWork;
        _ticketFacade = ticketFacade;
    }

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _unitOfWork.Tickets.GetAllAsync(cancellationToken);
        return await _ticketFacade.GetListOfFullTicketInfoAsync(tickets, cancellationToken);
    }
}