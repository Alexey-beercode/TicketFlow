using AnalyticsNotificationService.BLL.DTOs.Response.Analytics;

namespace AnalyticsNotificationService.BLL.Interfaces;

public interface IAnalyticsService
{
    Task<IEnumerable<AnalyticsResponseDto>> GetAllAnalyticsAsync();
    Task<AnalyticsResponseDto> GetAnalyticsByIdAsync(Guid id);
    Task<IEnumerable<AnalyticsResponseDto>> GetAnalyticsByMetricTypeAsync(Guid metricTypeId);
    Task DeleteAnalyticsAsync(Guid id);
}