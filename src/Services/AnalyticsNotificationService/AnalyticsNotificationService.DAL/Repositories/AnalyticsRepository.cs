using AnalyticsNotificationService.DLL.Infrastructure.Database;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using MongoDB.Driver;

namespace AnalyticsNotificationService.DLL.Repositories;

public class AnalyticsRepository : BaseRepository<Analytics>, IAnalyticsRepository
{
    public AnalyticsRepository(NotificationDbContext context) 
        : base(context, "Analytics") { }

    public async Task<IEnumerable<Analytics>> GetByMetricTypeIdAsync(Guid metricTypeId)
    {
        return await _collection.Find(a => a.MetricTypeId == metricTypeId && !a.IsDeleted).ToListAsync();
    }
}