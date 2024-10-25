using AnalyticsNotificationService.BLL.DTOs.Request.Notification;
using AnalyticsNotificationService.BLL.DTOs.Response.Notification;
using AnalyticsNotificationService.Domain.Entities;

namespace AnalyticsNotificationService.BLL.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationResponseDto>> GetUserNotificationsAsync(Guid userId);
    Task<IEnumerable<NotificationResponseDto>> GetAllNotificationsAsync();
    Task CreateNotificationAsync(CreateNotificationDto notificationDto);
    Task DeleteNotificationAsync(Guid notificationId);
}