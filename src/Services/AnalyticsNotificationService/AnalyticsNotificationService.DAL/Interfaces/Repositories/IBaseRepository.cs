using AnalyticsNotificationService.Domain.Common;

namespace AnalyticsNotificationService.DLL.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    IAsyncEnumerable<T> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
}