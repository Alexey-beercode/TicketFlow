namespace AnalyticsNotificationService.BLL.DTOs.Response.Notification;

public class NotificationResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
}