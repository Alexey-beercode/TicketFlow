using AnalyticsNotificationService.BLL.Interfaces;
using AnalyticsNotificationService.Domain.Enums;
using AnalyticsNotificationService.Domain.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AnalyticsNotificationService.BLL.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailTemplateService _emailTemplateService;

    public EmailService(IConfiguration configuration, IEmailTemplateService emailTemplateService)
    {
        _configuration = configuration;
        _emailTemplateService = emailTemplateService;
    }
    
    public async Task SendEmailAsync<T>(string toEmail, T model, NotificationType type,
        CancellationToken cancellationToken = default)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("TicketFlow", _configuration["Email:SenderAddress"]));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = "Новое сообщение от TicketFlow";
        
        var bodyBuilder = new BodyBuilder { HtmlBody = GetTemplate(type,model) };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:SmtpPort"]), true, cancellationToken);
            await client.AuthenticateAsync(_configuration["Email:SmtpUser"], _configuration["Email:SmtpPassword"],cancellationToken);
            await client.SendAsync(emailMessage,cancellationToken);
            await client.DisconnectAsync(true,cancellationToken);
        }
    }

    private string GetTemplate<T>(NotificationType type, T model)
    {
        string emailMessageTemplate = type switch
        {
            NotificationType.Welcome => model is WelcomeNotificationModel welcomeModel 
                ? _emailTemplateService.GetWelcomeTemplate(welcomeModel)
                : throw new ArgumentException("Invalid model type for Welcome notification"),
            
            NotificationType.TicketPurchase => model is TicketPurchaseNotificationModel purchaseModel 
                ? _emailTemplateService.GetTicketPurchaseTemplate(purchaseModel)
                : throw new ArgumentException("Invalid model type for TicketPurchase notification"),
            
            NotificationType.GeneralNotification => model is GeneralNotificationModel generalModel 
                ? _emailTemplateService.GetGeneralNotificationTemplate(generalModel)
                : throw new ArgumentException("Invalid model type for GeneralNotification notification"),
            
            _ => throw new ArgumentException("Unknown notification type")
        };
        
        return emailMessageTemplate;
    }
}