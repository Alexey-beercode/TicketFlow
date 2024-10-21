using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.UseCases.Trip.GetAll;
using BookingService.Application.UseCases.Trip.GetByFilter;
using BookingService.Application.UseCases.Trip.GetById;
using BookingService.Application.UseCases.Trip.GetByTypeId;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<TripController> _logger;

    public TripController(IMediator mediator, ILogger<TripController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<TripController>(logger);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetAllTrips()
    {
        _logger.LogStartRequest("Get All Trips");
        var result = await _mediator.Send(new GetAllTripsQuery());
        _logger.LogEndOfOperation("Get All Trips", "retrieved all trips");
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripResponseDto>> GetTripById(Guid id)
    {
        _logger.LogStartRequest("Get Trip by ID", "id", id.ToString());
        var result = await _mediator.Send(new GetTripByIdQuery { Id = id });
        _logger.LogEndOfOperation("Get Trip by ID", "retrieved trip");
        return Ok(result);
    }

    [HttpGet("filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsByFilter([FromQuery] GetTripsByFilterQuery query)
    {
        _logger.LogStartRequest("Get Trips by Filter");
        var result = await _mediator.Send(query);
        _logger.LogEndOfOperation("Get Trips by Filter", "retrieved filtered trips");
        return Ok(result);
    }

    [HttpGet("type/{typeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripResponseDto>>> GetTripsByTypeId(Guid typeId)
    {
        _logger.LogStartRequest("Get Trips by Type ID", "typeId", typeId.ToString());
        var result = await _mediator.Send(new GetTripsByTypeIdQuery { TypeId = typeId });
        _logger.LogEndOfOperation("Get Trips by Type ID", "retrieved trips");
        return Ok(result);
    }
}