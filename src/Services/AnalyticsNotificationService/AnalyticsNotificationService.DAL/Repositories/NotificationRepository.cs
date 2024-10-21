using AnalyticsNotificationService.DLL.Infrastructure.Database;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using MongoDB.Driver;

namespace AnalyticsNotificationService.DLL.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(NotificationDbContext context) 
        : base(context, "Notifications") { }

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId)
    {
        return await _collection.Find(n => n.UserId == userId && !n.IsDeleted).ToListAsync();
    }
}