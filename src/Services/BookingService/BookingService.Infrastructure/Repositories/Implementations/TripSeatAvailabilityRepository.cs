using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TripSeatAvailabilityRepository:BaseRepository<TripSeatAvailability>,ITripSeatAvailabilityRepository
{
    public TripSeatAvailabilityRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }

    public Task<List<TripSeatAvailability>> GetByTripIdAsync(Guid tripId, CancellationToken cancellationToken = default)
    {
        return _dbSet.Where(availability => availability.TripId == tripId && !availability.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}