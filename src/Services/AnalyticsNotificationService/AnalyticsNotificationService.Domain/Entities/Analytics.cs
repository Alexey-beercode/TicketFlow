using AnalyticsNotificationService.Domain.Common;

namespace AnalyticsNotificationService.Domain.Entities;

public class Analytics : BaseEntity
{
    public Guid MetricTypeId { get; set; }
    public long MetricValue { get; set; }
}