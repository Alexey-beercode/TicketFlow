using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ITripSeatAvailabilityRepository:IBaseRepository<TripSeatAvailability>
{
    Task<List<TripSeatAvailability>> GetByTripIdAsync(Guid tripId, CancellationToken cancellationToken = default);

    Task<TripSeatAvailability> GetAvailableByTripIdAndSeatTypeIdAsync(Guid tripId, Guid seatTypeId,
        CancellationToken cancellationToken = default);
}