using AnalyticsNotificationService.Domain.Entities;

namespace AnalyticsNotificationService.DLL.Interfaces.Repositories;

public interface IMetricTypeRepository : IBaseRepository<MetricType>
{
    Task<MetricType?> GetByNameAsync(string name);
}