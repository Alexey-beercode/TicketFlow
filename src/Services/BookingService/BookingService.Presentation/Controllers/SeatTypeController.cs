using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.UseCases.SeatType.GetAll;
using BookingService.Application.UseCases.SeatType.GetById;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/seat-types")]
public class SeatTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<SeatTypeController> _logger;

    public SeatTypeController(IMediator mediator, ILogger<SeatTypeController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<SeatTypeController>(logger);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SeatTypeResponseDto>>> GetAllSeatTypes()
    {
        _logger.LogStartRequest("Get All Seat Types");
        var result = await _mediator.Send(new GetAllQuery());
        _logger.LogEndOfOperation("Get All Seat Types", "retrieved all seat types");
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeatTypeResponseDto>> GetSeatTypeById(Guid id)
    {
        _logger.LogStartRequest("Get Seat Type by ID", "id", id.ToString());
        var result = await _mediator.Send(new GetByIdQuery { Id = id });
        if (result == null)
        {
            return NotFound();
        }
        _logger.LogEndOfOperation("Get Seat Type by ID", "retrieved seat type");
        return Ok(result);
    }
}