using AnalyticsNotificationService.Domain.Enums;

namespace AnalyticsNotificationService.BLL.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync<T>(string toEmail, T model, NotificationType type, CancellationToken cancellationToken = default);
}