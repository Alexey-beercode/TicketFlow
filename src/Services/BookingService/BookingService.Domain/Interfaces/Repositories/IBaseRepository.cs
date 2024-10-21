using BookingService.Domain.Common;
using BookingService.Domain.Interfaces.Entities;
using UserService.Domain.Interfaces;

namespace BookingService.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid? id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
}