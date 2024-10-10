using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ITripRepository:IBaseRepository<Trip>
{
    Task<IEnumerable<Trip>> GetByTypeIdAsync(Guid typeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Trip>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken = default);
    Task<IEnumerable<Trip>> GetByDepartureCityAsync(string departureCity, CancellationToken cancellationToken = default);
    Task<IEnumerable<Trip>> GetByArrivalCityAsync(string arrivalCity, CancellationToken cancellationToken = default);
    Task<IEnumerable<Trip>> GetByDepartureDateAsync(DateTime departureDate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Trip>> SearchTripsAsync(string departureCity, string arrivalCity, DateTime departureDate, CancellationToken cancellationToken = default);
}