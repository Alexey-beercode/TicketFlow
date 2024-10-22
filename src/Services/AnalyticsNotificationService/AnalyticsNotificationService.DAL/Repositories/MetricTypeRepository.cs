using AnalyticsNotificationService.DLL.Infrastructure.Database;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using MongoDB.Driver;

namespace AnalyticsNotificationService.DLL.Repositories;

public class MetricTypeRepository : BaseRepository<MetricType>, IMetricTypeRepository
{
    public MetricTypeRepository(NotificationDbContext context) 
        : base(context, "MetricTypes") { }

    public async Task<MetricType?> GetByNameAsync(string name)
    {
        return await _collection.Find(m => m.Name == name && !m.IsDeleted).FirstOrDefaultAsync();
    }
}