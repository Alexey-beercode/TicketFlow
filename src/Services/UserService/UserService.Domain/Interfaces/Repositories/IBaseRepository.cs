namespace UserService.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T:IHasId,ISoftDeletable
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
}