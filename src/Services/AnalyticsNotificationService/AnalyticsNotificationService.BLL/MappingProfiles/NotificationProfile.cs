using AnalyticsNotificationService.BLL.DTOs.Request.Notification;
using AnalyticsNotificationService.BLL.DTOs.Response.Notification;
using AnalyticsNotificationService.Domain.Entities;
using AnalyticsNotificationService.Domain.Models;
using AutoMapper;

namespace AnalyticsNotificationService.BLL.MappingProfiles;

public class NotificationProfile:Profile
{
    public NotificationProfile()
    {
        CreateMap<CreateNotificationDto, Notification>();
        CreateMap<CreateNotificationDto, GeneralNotificationModel>();
        CreateMap<Notification, NotificationResponseDto>();
    }
}