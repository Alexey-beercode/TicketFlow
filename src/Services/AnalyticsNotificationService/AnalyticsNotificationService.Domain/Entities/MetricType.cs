using AnalyticsNotificationService.Domain.Interfaces;

namespace AnalyticsNotificationService.Domain.Entities;

public class MetricType : BaseEntity
{
    public string Name { get; set; }
}