using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ITicketRepository:IBaseRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Ticket>> GetByStatusIdAsync(Guid statusId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Ticket>> GetByTripIdAsync(Guid tripAsync, CancellationToken cancellationToken = default);
    Task<IEnumerable<Ticket>> GetByCouponIdAsync(Guid couponId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Ticket>> GetBySeatTypeIdAsync(Guid seatTypeId, CancellationToken cancellationToken = default);
    void Delete(Ticket ticket);
}