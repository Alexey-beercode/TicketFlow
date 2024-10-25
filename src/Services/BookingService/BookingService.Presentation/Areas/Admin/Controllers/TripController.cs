using BookingService.Application.UseCases.Trip.Create;
using BookingService.Application.UseCases.Trip.Delete;
using BookingService.Presentation.Controllers;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Areas.Admin.Controllers;

[ApiController]
[Route("api/admin/trips")]
public class AdminTripController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<AdminTripController> _logger;

    public AdminTripController(IMediator mediator, ILogger<AdminTripController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<AdminTripController>(logger);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand command)
    {
        _logger.LogStartRequest("Create Trip");
        await _mediator.Send(command);
        _logger.LogEndOfOperation("Create Trip", "created trip");
        return CreatedAtAction(nameof(TripController.GetTripById), "Trip", new { id = Guid.NewGuid() }, null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrip(Guid id)
    {
        _logger.LogStartRequest("Delete Trip", "id", id.ToString());
        await _mediator.Send(new DeleteTripCommand { Id = id });
        _logger.LogEndOfOperation("Delete Trip", "deleted trip");
        return NoContent();
    }
}