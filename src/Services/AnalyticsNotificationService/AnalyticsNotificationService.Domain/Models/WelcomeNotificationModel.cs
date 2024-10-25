namespace AnalyticsNotificationService.Domain.Models;

public class WelcomeNotificationModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}