using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetByUserId;

public class GetByUserIdHandler:IRequestHandler<GetByUserIdQuery,IEnumerable<TicketResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITicketFacade _ticketFacade;

    public GetByUserIdHandler(IUnitOfWork unitOfWork, ITicketFacade ticketFacade)
    {
        _unitOfWork = unitOfWork;
        _ticketFacade = ticketFacade;
    }

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _unitOfWork.Tickets.GetByUserIdAsync(request.UserId);
        
        var ticketsDtos =await _ticketFacade.GetListOfFullTicketInfoAsync(tickets, cancellationToken);
        return ticketsDtos;
    }
}