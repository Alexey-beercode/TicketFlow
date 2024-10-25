using AnalyticsNotificationService.BLL.DTOs.Request.Notification;
using AnalyticsNotificationService.BLL.DTOs.Response.Notification;
using AnalyticsNotificationService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsNotificationService.Controllers;

[Authorize("Admin")]
[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NotificationResponseDto>>> GetAllNotifications()
    {
        _logger.LogInformation("Getting all notifications");
        var result = await _notificationService.GetAllNotificationsAsync();
        _logger.LogInformation("Retrieved all notifications successfully");
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NotificationResponseDto>>> GetUserNotifications(Guid userId)
    {
        _logger.LogInformation("Getting notifications for user ID: {UserId}", userId);
        var result = await _notificationService.GetUserNotificationsAsync(userId);
        _logger.LogInformation("Retrieved notifications for user ID: {UserId}", userId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto notificationDto)
    {
        _logger.LogInformation("Creating new notification for email: {Email}", notificationDto.Email);
        await _notificationService.CreateNotificationAsync(notificationDto);
        _logger.LogInformation("Created notification and sent email to: {Email}", notificationDto.Email);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        _logger.LogInformation("Deleting notification with ID: {Id}", id);
        await _notificationService.DeleteNotificationAsync(id);
        _logger.LogInformation("Deleted notification with ID: {Id}", id);
        return NoContent();
    }
}