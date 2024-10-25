using AnalyticsNotificationService.BLL.DTOs.Response.Analytics;
using AnalyticsNotificationService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsNotificationService.Controllers;

[Authorize("Admin")]
[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AnalyticsResponseDto>>> GetAllAnalytics()
    {
        _logger.LogInformation("Getting all analytics");
        var result = await _analyticsService.GetAllAnalyticsAsync();
        _logger.LogInformation("Retrieved all analytics successfully");
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AnalyticsResponseDto>> GetAnalyticsById(Guid id)
    {
        _logger.LogInformation("Getting analytics by ID: {Id}", id);
        var result = await _analyticsService.GetAnalyticsByIdAsync(id);
        _logger.LogInformation("Retrieved analytics with ID: {Id}", id);
        return Ok(result);
    }

    [HttpGet("by-metric-type/{metricTypeId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AnalyticsResponseDto>>> GetAnalyticsByMetricType(Guid metricTypeId)
    {
        _logger.LogInformation("Getting analytics by metric type ID: {MetricTypeId}", metricTypeId);
        var result = await _analyticsService.GetAnalyticsByMetricTypeAsync(metricTypeId);
        _logger.LogInformation("Retrieved analytics for metric type ID: {MetricTypeId}", metricTypeId);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAnalytics(Guid id)
    {
        _logger.LogInformation("Deleting analytics with ID: {Id}", id);
        await _analyticsService.DeleteAnalyticsAsync(id);
        _logger.LogInformation("Deleted analytics with ID: {Id}", id);
        return NoContent();
    }
}