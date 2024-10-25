namespace AnalyticsNotificationService.Domain.Models;

public class TicketPurchaseNotificationModel
{
    public decimal Price { get; set; }
    public string UserName { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string TripName { get; set; }
    public string SeatType { get; set; }
}