using BookingService.Application.DTOs.Response.TripType;
using BookingService.Application.UseCases.TripType.GetAll;
using BookingService.Application.UseCases.TripType.GetById;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/trip-types")]
public class TripTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<TripTypeController> _logger;

    public TripTypeController(IMediator mediator, ILogger<TripTypeController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<TripTypeController>(logger);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripTypeResponseDto>>> GetAllTripTypes()
    {
        _logger.LogStartRequest("Get All Trip Types");
        var result = await _mediator.Send(new GetAllQuery());
        _logger.LogEndOfOperation("Get All Trip Types", "retrieved all trip types");
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripTypeResponseDto>> GetTripTypeById(Guid id)
    {
        _logger.LogStartRequest("Get Trip Type by ID", "id", id.ToString());
        var result = await _mediator.Send(new GetByIdQuery { Id = id });
        _logger.LogEndOfOperation("Get Trip Type by ID", "retrieved trip type");
        return Ok(result);
    }
}