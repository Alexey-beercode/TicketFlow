using BookingService.Application.UseCases.DIscountType.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/discount-types")]
public class DiscountTypeController:ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var discountTypes = await _mediator.Send(new GetAllDiscountTypesQuery());
        return Ok(discountTypes);
    }
}