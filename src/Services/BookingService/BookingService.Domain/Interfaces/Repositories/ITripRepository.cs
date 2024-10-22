using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Specifications;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ITripRepository:IBaseRepository<Trip>
{
    Task<IEnumerable<Trip>> GetBySpecificationAsync(ISpecification<Trip> specification, CancellationToken cancellationToken = default);
    Task DeleteAsync(Trip trip, CancellationToken cancellationToken = default);
}