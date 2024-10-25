using AnalyticsNotificationService.Domain.Entities;

namespace AnalyticsNotificationService.DLL.Interfaces.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId);
}