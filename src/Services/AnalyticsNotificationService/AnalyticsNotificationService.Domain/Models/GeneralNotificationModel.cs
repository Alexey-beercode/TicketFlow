﻿namespace AnalyticsNotificationService.Domain.Models;

public class GeneralNotificationModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    public string Email { get; set; }
}