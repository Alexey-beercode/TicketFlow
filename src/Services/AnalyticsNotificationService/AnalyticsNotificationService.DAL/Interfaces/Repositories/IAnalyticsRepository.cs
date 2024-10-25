using AnalyticsNotificationService.Domain.Entities;

namespace AnalyticsNotificationService.DLL.Interfaces.Repositories;

public interface IAnalyticsRepository : IBaseRepository<Analytics>
{
    Task<IEnumerable<Analytics>> GetByMetricTypeIdAsync(Guid metricTypeId);
}