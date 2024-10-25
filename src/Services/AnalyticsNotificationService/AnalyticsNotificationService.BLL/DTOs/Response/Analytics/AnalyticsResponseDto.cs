using AnalyticsNotificationService.BLL.DTOs.Response.MetricType;

namespace AnalyticsNotificationService.BLL.DTOs.Response.Analytics;

public class AnalyticsResponseDto
{
    public Guid Id { get; set; }
    public long MetricValue { get; set; }
    public MetricTypeResponseDto MetricType { get; set; }
}