using AnalyticsNotificationService.Domain.Interfaces;

namespace AnalyticsNotificationService.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Message { get; set; }
}