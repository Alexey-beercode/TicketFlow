using AnalyticsNotificationService.BLL.DTOs.Response.MetricType;

namespace AnalyticsNotificationService.BLL.Interfaces;

public interface IMetricTypeService
{
    Task<IEnumerable<MetricTypeResponseDto>> GetAllMetricTypesAsync();
    Task<MetricTypeResponseDto> GetMetricTypeByIdAsync(Guid id);
    Task<MetricTypeResponseDto> GetMetricTypeByNameAsync(string name);
    Task CreateMetricTypeAsync(string name);
    Task DeleteMetricTypeAsync(Guid id);
}