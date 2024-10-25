using AnalyticsNotificationService.Domain.Models;

namespace AnalyticsNotificationService.BLL.Interfaces;

public interface IEmailTemplateService
{
    string GetWelcomeTemplate(WelcomeNotificationModel model);
    string GetTicketPurchaseTemplate(TicketPurchaseNotificationModel model);
    string GetGeneralNotificationTemplate(GeneralNotificationModel model);
}