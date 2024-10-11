using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.GetActiveCoupons;

public class GetActiveCouponsHandler:IRequestHandler<GetActiveCouponsQuery,IEnumerable<CouponResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetActiveCouponsHandler> _logger;

    public GetActiveCouponsHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetActiveCouponsHandler> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<CouponResponseDto>> Handle(GetActiveCouponsQuery request, CancellationToken cancellationToken)
    {
        var activeCoupons = await _unitOfWork.Coupons.GetActiveCouponsAsync(cancellationToken);
        _logger.LogInformation("Getting active coupons");

        return _mapper.Map<IEnumerable<CouponResponseDto>>(activeCoupons);
    }
}