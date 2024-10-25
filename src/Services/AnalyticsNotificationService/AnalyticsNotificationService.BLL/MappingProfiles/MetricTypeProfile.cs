using AnalyticsNotificationService.BLL.DTOs.Response.MetricType;
using AnalyticsNotificationService.Domain.Entities;
using AutoMapper;

namespace AnalyticsNotificationService.BLL.MappingProfiles;

public class MetricTypeProfile:Profile
{
    public MetricTypeProfile()
    {
        CreateMap<MetricType, MetricTypeResponseDto>();
    }
}