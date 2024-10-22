using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetByStatusId;

public class GetByStatusIdHandler:IRequestHandler<GetByStatusIdQuery,IEnumerable<TicketResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketFacade _ticketFacade;

    public GetByStatusIdHandler(IUnitOfWork unitOfWork, ITicketFacade ticketFacade)
    {
        _unitOfWork = unitOfWork;
        _ticketFacade = ticketFacade;
    }

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetByStatusIdQuery request, CancellationToken cancellationToken)
    {
        var ticketsByStatus = await _unitOfWork.Tickets.GetByStatusIdAsync(request.StatusId, cancellationToken);

        var ticketsDtos = await _ticketFacade.GetListOfFullTicketInfoAsync(ticketsByStatus, cancellationToken);
        return ticketsDtos;
    }
}