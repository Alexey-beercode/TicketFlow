using AnalyticsNotificationService.BLL.DTOs.Response.MetricType;
using AnalyticsNotificationService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsNotificationService.Controllers;

[Authorize("Admin")]
[ApiController]
[Route("api/metric-types")]
public class MetricTypeController : ControllerBase
{
    private readonly IMetricTypeService _metricTypeService;
    private readonly ILogger<MetricTypeController> _logger;

    public MetricTypeController(IMetricTypeService metricTypeService, ILogger<MetricTypeController> logger)
    {
        _metricTypeService = metricTypeService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MetricTypeResponseDto>>> GetAllMetricTypes()
    {
        _logger.LogInformation("Getting all metric types");
        var result = await _metricTypeService.GetAllMetricTypesAsync();
        _logger.LogInformation("Retrieved all metric types successfully");
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MetricTypeResponseDto>> GetMetricTypeById(Guid id)
    {
        _logger.LogInformation("Getting metric type by ID: {Id}", id);
        var result = await _metricTypeService.GetMetricTypeByIdAsync(id);
        _logger.LogInformation("Retrieved metric type with ID: {Id}", id);
        return Ok(result);
    }

    [HttpGet("by-name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MetricTypeResponseDto>> GetMetricTypeByName(string name)
    {
        _logger.LogInformation("Getting metric type by name: {Name}", name);
        var result = await _metricTypeService.GetMetricTypeByNameAsync(name);
        _logger.LogInformation("Retrieved metric type with name: {Name}", name);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMetricType([FromBody] string name)
    {
        _logger.LogInformation("Creating new metric type with name: {Name}", name);
        await _metricTypeService.CreateMetricTypeAsync(name);
        _logger.LogInformation("Created new metric type with name: {Name}", name);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMetricType(Guid id)
    {
        _logger.LogInformation("Deleting metric type with ID: {Id}", id);
        await _metricTypeService.DeleteMetricTypeAsync(id);
        _logger.LogInformation("Deleted metric type with ID: {Id}", id);
        return NoContent();
    }
}