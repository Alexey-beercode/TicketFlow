using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TripSeatAvailabilityRepository:BaseRepository<TripSeatAvailability>,ITripSeatAvailabilityRepository
{
    public TripSeatAvailabilityRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }
}