using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.GetAll;

public class GetAllCouponsHandler:IRequestHandler<GetAllCouponsQuery,IEnumerable<CouponResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllCouponsHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllCouponsHandler(IUnitOfWork unitOfWork, ILogger<GetAllCouponsHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CouponResponseDto>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken=default)
    {
        var coupons = await _unitOfWork.Coupons.GetAllAsync(cancellationToken);
        _logger.LogInformation("Getting all coupons");
        
        return _mapper.Map<IEnumerable<CouponResponseDto>>(coupons);
    }
}