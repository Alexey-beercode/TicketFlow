using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.UseCases.Ticket.Create;
using BookingService.Application.UseCases.Ticket.GetById;
using BookingService.Application.UseCases.Ticket.GetByUserId;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<TicketController> _logger;

    public TicketController(IMediator mediator, ILogger<TicketController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<TicketController>(logger);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
    {
        _logger.LogStartRequest("Create Ticket");
        await _mediator.Send(command);
        _logger.LogEndOfOperation("Create Ticket", "created ticket");
        return CreatedAtAction(nameof(GetTicketById), new { id = command.UserId }, null);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketResponseDto>> GetTicketById(Guid id)
    {
        _logger.LogStartRequest("Get Ticket by ID", "id", id.ToString());
        var result = await _mediator.Send(new GetByIdQuery { Id = id });
        _logger.LogEndOfOperation("Get Ticket by ID", "retrieved ticket");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetTicketsByUserId(Guid userId)
    {
        _logger.LogStartRequest("Get Tickets by User ID", "userId", userId.ToString());
        var result = await _mediator.Send(new GetByUserIdQuery { UserId = userId });
        _logger.LogEndOfOperation("Get Tickets by User ID", "retrieved tickets");
        return Ok(result);
    }
}