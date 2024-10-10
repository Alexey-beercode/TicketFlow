using BookingService.Domain.Interfaces.Repositories;

namespace BookingService.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    ICouponRepository Coupons { get; }
    IDiscountTypeRepository DiscountTypes { get; }
    ISeatTypeRepository SeatTypes { get; }
    ITicketRepository Tickets { get; }
    ITicketStatusRepository TicketStatuses { get; }
    ITripRepository Trips { get; }
    ITripSeatAvailabilityRepository TripSeatAvailabilities { get; }
    ITripTypeRepository TripTypes { get; }
}