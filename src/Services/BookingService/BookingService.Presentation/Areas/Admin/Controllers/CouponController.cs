using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.UseCases.Coupon.Create;
using BookingService.Application.UseCases.Coupon.Delete;
using BookingService.Application.UseCases.Coupon.GetAll;
using BookingService.Application.UseCases.Coupon.GetByCode;
using BookingService.Application.UseCases.Coupon.GetByDiscountTypeId;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "Admin")]
[Route("api/admin/coupons")]
public class AdminCouponController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<AdminCouponController> _logger;

    public AdminCouponController(IMediator mediator, ILogger<AdminCouponController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<AdminCouponController>(logger);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponCommand command)
    {
        _logger.LogStartRequest("Create Coupon");
        await _mediator.Send(command);
        _logger.LogEndOfOperation("Create Coupon", "created coupon");
        return CreatedAtAction(nameof(GetCouponByCode), new { code = command.Code }, null);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCoupon(Guid id)
    {
        _logger.LogStartRequest("Delete Coupon", "id", id.ToString());
        await _mediator.Send(new DeleteCouponCommand { Id = id });
        _logger.LogEndOfOperation("Delete Coupon", "deleted coupon");
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CouponResponseDto>>> GetAllCoupons()
    {
        _logger.LogStartRequest("Get All Coupons");
        var result = await _mediator.Send(new GetAllCouponsQuery());
        _logger.LogEndOfOperation("Get All Coupons", "retrieved all coupons");
        return Ok(result);
    }

    [HttpGet("by-discount-type/{discountTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CouponResponseDto>>> GetCouponsByDiscountType(Guid discountTypeId)
    {
        _logger.LogStartRequest("Get Coupons by Discount Type ID", "discountTypeId", discountTypeId.ToString());
        var result = await _mediator.Send(new GetByDiscountTypeIdQuery { DiscountTypeId = discountTypeId });
        _logger.LogEndOfOperation("Get Coupons by Discount Type ID", "retrieved coupons");
        return Ok(result);
    }

    [HttpGet("by-code/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CouponResponseDto>> GetCouponByCode(string code)
    {
        _logger.LogStartRequest("Get Coupon by Code", "code", code);
        var result = await _mediator.Send(new GetByCodeQuery { Code = code });
        _logger.LogEndOfOperation("Get Coupon by Code", "retrieved coupon");
        return Ok(result);
    }
}