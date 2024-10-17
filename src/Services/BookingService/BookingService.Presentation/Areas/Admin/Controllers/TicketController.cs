using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.UseCases.Ticket.Delete;
using BookingService.Application.UseCases.Ticket.GetBySeatType;
using BookingService.Application.UseCases.Ticket.GetByStatusId;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "Admin")]
[Route("api/admin/tickets")]
public class AdminTicketController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<AdminTicketController> _logger;

    public AdminTicketController(IMediator mediator, ILogger<AdminTicketController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<AdminTicketController>(logger);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTicket(Guid id)
    {
        _logger.LogStartRequest("Delete Ticket", "id", id.ToString());
        await _mediator.Send(new DeleteTicketCommand { TicketId = id });
        _logger.LogEndOfOperation("Delete Ticket", "deleted ticket");
        return NoContent();
    }

    [HttpGet("seat-type/{seatTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetTicketsBySeatType(Guid seatTypeId)
    {
        _logger.LogStartRequest("Get Tickets by Seat Type", "seatTypeId", seatTypeId.ToString());
        var result = await _mediator.Send(new GetTicketBySeatTypeIdQuery { SeatTypeId = seatTypeId });
        _logger.LogEndOfOperation("Get Tickets by Seat Type", "retrieved tickets");
        return Ok(result);
    }

    [HttpGet("status/{statusId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetTicketsByStatus(Guid statusId)
    {
        _logger.LogStartRequest("Get Tickets by Status", "statusId", statusId.ToString());
        var result = await _mediator.Send(new GetByStatusIdQuery { StatusId = statusId });
        _logger.LogEndOfOperation("Get Tickets by Status", "retrieved tickets");
        return Ok(result);
    }
}