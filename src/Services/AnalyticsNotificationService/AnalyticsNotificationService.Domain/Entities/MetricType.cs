using AnalyticsNotificationService.Domain.Common;

namespace AnalyticsNotificationService.Domain.Entities;

public class MetricType : BaseEntity
{
    public string Name { get; set; }
}