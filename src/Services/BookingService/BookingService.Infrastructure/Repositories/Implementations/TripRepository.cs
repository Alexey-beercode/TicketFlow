using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Interfaces.Specifications;
using BookingService.Domain.Specifications.Trip;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TripRepository : BaseRepository<Trip>, ITripRepository
{
    private readonly BookingDbContext _dbContext;

    public TripRepository(BookingDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Trip>> GetBySpecificationAsync(ISpecification<Trip> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Trip trip, CancellationToken cancellationToken = default)
    {
        trip.IsDeleted = true;
        _dbContext.Trips.Update(trip);
            
        await _dbContext.Tickets
            .Where(ticket => ticket.TripId == trip.Id && !ticket.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(ticket => ticket.IsDeleted, true), cancellationToken);

        await _dbContext.TripSeatAvailabilities
            .Where(availability => availability.TripId == trip.Id && !availability.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(availability => availability.IsDeleted, true), cancellationToken);
    }

    private IQueryable<Trip> ApplySpecification(ISpecification<Trip> spec)
    {
        return _dbContext.Set<Trip>().Where(spec.Criteria);
    }
}