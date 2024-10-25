using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.UseCases.Coupon.GetActiveCoupons;
using BookingService.Application.UseCases.Coupon.GetByCode;
using BookingService.Application.UseCases.Coupon.GetByUserId;
using BookingService.Application.UseCases.Coupon.GetUsedByUser;
using BookingService.Presentation.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers;

[ApiController]
[Route("api/coupons")]
public class CouponController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoggerHelper<CouponController> _logger;

    public CouponController(IMediator mediator, ILogger<CouponController> logger)
    {
        _mediator = mediator;
        _logger = new LoggerHelper<CouponController>(logger);
    }

    [HttpGet("active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CouponResponseDto>>> GetActiveCoupons()
    {
        _logger.LogStartRequest("Get Active Coupons");
        var result = await _mediator.Send(new GetActiveCouponsQuery());
        _logger.LogEndOfOperation("Get Active Coupons", "retrieved active coupons");
        return Ok(result);
    }

    [Authorize]
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

    [Authorize]
    [HttpGet("by-user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CouponResponseDto>>> GetCouponsByUser(Guid userId)
    {
        _logger.LogStartRequest("Get Coupons by User ID", "userId", userId.ToString());
        var result = await _mediator.Send(new GetByUserIdQuery { UserId = userId });
        _logger.LogEndOfOperation("Get Coupons by User ID", "retrieved coupons");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("used-by-user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CouponResponseDto>>> GetUsedCouponsByUser(Guid userId)
    {
        _logger.LogStartRequest("Get Used Coupons by User", "userId", userId.ToString());
        var result = await _mediator.Send(new GetUsedByUserQuery { UserId = userId });
        _logger.LogEndOfOperation("Get Used Coupons by User", "retrieved used coupons");
        return Ok(result);
    }
}