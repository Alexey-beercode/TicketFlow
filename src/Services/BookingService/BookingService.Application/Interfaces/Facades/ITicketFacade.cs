using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Domain.Entities;

namespace BookingService.Application.Interfaces.Facades;

public interface ITicketFacade
{
    Task<TicketResponseDto> GetFullTicketInfoAsync(Ticket ticket,CancellationToken cancellationToken=default);

    Task<List<TicketResponseDto>> GetListOfFullTicketInfoAsync(IEnumerable<Ticket> tickets,
        CancellationToken cancellationToken = default);
}