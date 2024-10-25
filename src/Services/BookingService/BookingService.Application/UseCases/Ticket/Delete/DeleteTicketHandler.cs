using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.Delete;

public class DeleteTicketHandler:IRequestHandler<DeleteTicketCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(request.TicketId, cancellationToken);
        var cancelledTicketStatus = await _unitOfWork.TicketStatuses.GetByNameAsync("Cancelled");
        ticket.StatusId = cancelledTicketStatus.Id;

        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _unitOfWork.Tickets.Delete(ticket);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}