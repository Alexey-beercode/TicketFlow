using AnalyticsNotificationService.BLL.DTOs.Response.Analytics;
using AnalyticsNotificationService.Domain.Entities;
using AutoMapper;

namespace AnalyticsNotificationService.BLL.MappingProfiles;

public class AnalyticsProfile:Profile
{
    public AnalyticsProfile()
    {
        CreateMap<Analytics, AnalyticsResponseDto>();
    }
}