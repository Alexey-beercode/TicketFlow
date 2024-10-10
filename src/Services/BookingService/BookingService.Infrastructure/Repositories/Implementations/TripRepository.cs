using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Interfaces.Specifications;
using BookingService.Domain.Specifications.Trip;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TripRepository : BaseRepository<Trip>, ITripRepository
{
    private readonly BookingDbContext _context;

    public TripRepository(BookingDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trip>> GetByTypeIdAsync(Guid typeId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Trip>()
            .Where(t => t.TypeId == typeId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken = default)
    {
        var spec = new TripByPriceRangeSpecification(minPrice, maxPrice);
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetByDepartureCityAsync(string departureCity, CancellationToken cancellationToken = default)
    {
        var spec = new TripByDepartureCitySpecification(departureCity);
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetByArrivalCityAsync(string arrivalCity, CancellationToken cancellationToken = default)
    {
        var spec = new TripByArrivalCitySpecification(arrivalCity);
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> GetByDepartureDateAsync(DateTime departureDate, CancellationToken cancellationToken = default)
    {
        var spec = new TripByDepartureDateSpecification(departureDate);
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Trip>> SearchTripsAsync(string departureCity, string arrivalCity, DateTime departureDate, CancellationToken cancellationToken = default)
    {
        var spec = new TripSearchSpecification(departureCity, arrivalCity, departureDate);
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(Trip trip, CancellationToken cancellationToken = default)
    {
        trip.IsDeleted = true;
        _dbContext.Trips.Update(trip);
            
        await _dbContext.Tickets
            .Where(ticket => ticket.TripId ==trip.Id && !ticket.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(ticket => ticket.IsDeleted, true), cancellationToken);

        await _dbContext.TripSeatAvailabilities
            .Where(availability => availability.TripId == trip.Id && !availability.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(availability => availability.IsDeleted, true));
    }

    private IQueryable<Trip> ApplySpecification(ISpecification<Trip> spec)
    {
        return _context.Set<Trip>().Where(spec.Criteria);
    }
}