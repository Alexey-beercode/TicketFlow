using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.Exceptions;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.GetById;

public class GetByIdHandler:IRequestHandler<GetByIdQuery,TicketResponseDto>
{
    private readonly ITicketFacade _ticketFacade;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdHandler(ITicketFacade ticketFacade, IUnitOfWork unitOfWork)
    {
        _ticketFacade = ticketFacade;
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketResponseDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(request.Id, cancellationToken);

        if (ticket is null)
        {
            throw new EntityNotFoundException("Ticket", request.Id);
        }

        return await _ticketFacade.GetFullTicketInfoAsync(ticket, cancellationToken);
    }
}