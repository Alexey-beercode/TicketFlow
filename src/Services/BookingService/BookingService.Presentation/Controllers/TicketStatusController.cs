using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.UseCases.TicketStatus.GetAll;
using BookingService.Application.UseCases.TicketStatus.GetById;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/ticket-statuses")]
public class TicketStatusController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<TicketStatusController> _logger;

    public TicketStatusController(IMediator mediator, ILogger<TicketStatusController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<TicketStatusController>(logger);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketStatusResponseDto>>> GetAllTicketStatuses()
    {
        _logger.LogStartRequest("Get All Ticket Statuses");
        var result = await _mediator.Send(new GetAllTicketStatusesQuery());
        _logger.LogEndOfOperation("Get All Ticket Statuses", "retrieved all ticket statuses");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketStatusResponseDto>> GetTicketStatusById(Guid id)
    {
        _logger.LogStartRequest("Get Ticket Status by ID", "id", id.ToString());
        var result = await _mediator.Send(new GetByIdQuery { Id = id });
        _logger.LogEndOfOperation("Get Ticket Status by ID", "retrieved ticket status");
        return Ok(result);
    }
}