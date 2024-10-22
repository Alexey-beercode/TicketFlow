using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.GetByUserId;

public class GetByUserIdHandler:IRequestHandler<GetByUserIdQuery,IEnumerable<CouponResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetByUserIdHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetByUserIdHandler(IMapper mapper, ILogger<GetByUserIdHandler> logger, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CouponResponseDto>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken=default)
    {
        var couponsByUser = await _unitOfWork.Coupons.GetByUserIdAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Getting coupons by user");
        
        return _mapper.Map<IEnumerable<CouponResponseDto>>(couponsByUser);
    }
}