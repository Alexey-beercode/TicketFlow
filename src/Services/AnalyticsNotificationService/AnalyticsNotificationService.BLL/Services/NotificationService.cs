using AnalyticsNotificationService.BLL.DTOs.Request.Notification;
using AnalyticsNotificationService.BLL.DTOs.Response.Notification;
using AnalyticsNotificationService.BLL.Interfaces;
using AnalyticsNotificationService.DLL.Interfaces.Repositories;
using AnalyticsNotificationService.Domain.Entities;
using AnalyticsNotificationService.Domain.Enums;
using AnalyticsNotificationService.Domain.Models;
using AutoMapper;
using BookingService.Application.Exceptions;

namespace AnalyticsNotificationService.BLL.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public NotificationService(
        IEmailService emailService, INotificationRepository notificationRepository, IMapper mapper)
    {
        _emailService = emailService;
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationResponseDto>> GetUserNotificationsAsync(Guid userId)
    {
        var notifications = await _notificationRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
    }

    public async Task<IEnumerable<NotificationResponseDto>> GetAllNotificationsAsync()
    {
        var notifications = new List<Notification>();
        await foreach (var notification in _notificationRepository.GetAllAsync())
        {
            notifications.Add(notification);
        }

        return _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
    }

    public async Task CreateNotificationAsync(CreateNotificationDto notificationDto)
    {
        var notification = _mapper.Map<Notification>(notificationDto);
        notification.Date = DateTime.UtcNow;

        await _notificationRepository.CreateAsync(notification);

        var notificationModel = _mapper.Map<GeneralNotificationModel>(notificationDto);
        await _emailService.SendEmailAsync(notificationDto.Email, notificationModel,
            NotificationType.GeneralNotification);
    }

    public async Task DeleteNotificationAsync(Guid notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);

        if (notification is null)
        {
            throw new EntityNotFoundException("Notification", notificationId);
        }
    }
}