using AnalyticsNotificationService.BLL.Interfaces;
using AnalyticsNotificationService.Domain.Enums;
using AnalyticsNotificationService.Domain.Models;

namespace AnalyticsNotificationService.BLL.Services;

public class EmailTemplateService:IEmailTemplateService
{
    public string GetWelcomeTemplate(WelcomeNotificationModel model)
    {
        return $@"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='UTF-8'>
            <title>Успешная регистрация</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                }}
                .container {{
                    width: 80%;
                    margin: auto;
                    padding: 10px;
                }}
                .header {{
                    font-size: 20px;
                    font-weight: bold;
                }}
                .sub-header {{
                    font-size: 18px;
                }}
            </style>
        </head>
        <body>
        <div class='container'>
            <p class='header'>Вы успешно зарегистрировались в сервисе TicketFlow</p>
            <h4>Здравствуйте, {model.UserName}!</h4>
            <p>Рады приветствовать вас в нашем сервисе!</p>
        </div>
        </body>
        </html>";
    }

    public string GetTicketPurchaseTemplate(TicketPurchaseNotificationModel model)
    {
        return $@"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='UTF-8'>
            <title>Успешная регистрация</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                }}
                .container {{
                    width: 80%;
                    margin: auto;
                    padding: 10px;
                }}
                .header {{
                    font-size: 20px;
                    font-weight: bold;
                }}
                .sub-header {{
                    font-size: 18px;
                }}
            </style>
        </head>
        <body>
        <div class='container'>
            <p class='header'>Вы успешно зарегистрировались в сервисе TicketFlow</p>
            <h4>Здравствуйте, {model.UserName}!</h4>
            <p>Рады приветствовать вас в нашем сервисе!</p>

        <div class='details'>
                <h4 class='sub-header'>Детали билета</h4>
                <p>Поездка: {model.TripName}</p>
                <p>Тип места: {model.SeatType}</p>
                <p>Цена: {model.Price}</p>
        </div>
        </div>
        </body>
        </html>";
    }

    public string GetGeneralNotificationTemplate(GeneralNotificationModel model)
    {
        return $@"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='UTF-8'>
            <title>Успешная регистрация</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                }}
                .container {{
                    width: 80%;
                    margin: auto;
                    padding: 10px;
                }}
                .header {{
                    font-size: 20px;
                    font-weight: bold;
                }}
                .sub-header {{
                    font-size: 18px;
                }}
            </style>
        </head>
        <body>
        <div class='container'>
            <p class='header'>Вы успешно зарегистрировались в сервисе TicketFlow</p>
            <h4>Здравствуйте, {model.UserName}!</h4>
            <p>Рады приветствовать вас в нашем сервисе!</p>

        <div class='details'>
                <h4 class='sub-header'>Хотим сообщить вам данную информацию :</h4>
                <p>{model.Message}</p>
        </div>
        </div>
        </body>
        </html>";
    }
}