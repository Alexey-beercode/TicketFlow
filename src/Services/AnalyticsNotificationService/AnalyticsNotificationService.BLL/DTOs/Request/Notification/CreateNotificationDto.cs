namespace AnalyticsNotificationService.BLL.DTOs.Request.Notification;

public class CreateNotificationDto
{
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}