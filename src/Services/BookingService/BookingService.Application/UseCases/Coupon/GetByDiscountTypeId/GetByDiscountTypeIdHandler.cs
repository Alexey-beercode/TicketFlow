using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.GetByDiscountTypeId;

public class GetByDiscountTypeIdHandler:IRequestHandler<GetByDiscountTypeIdQuery,IEnumerable<CouponResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetByDiscountTypeIdHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetByDiscountTypeIdHandler(IMapper mapper, ILogger<GetByDiscountTypeIdHandler> logger, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CouponResponseDto>> Handle(GetByDiscountTypeIdQuery request, CancellationToken cancellationToken)
    {
        var dicountType = await _unitOfWork.DiscountTypes.GetByIdAsync(request.DiscountTypeId, cancellationToken);

        if (dicountType is null)
        {
            throw new EntityNotFoundException("DiscountType", request.DiscountTypeId);
        }

        _logger.LogInformation("Getting coupons by discount type");
        
        var coupons = await _unitOfWork.Coupons.GetByDiscountTypeIdAsync(request.DiscountTypeId, cancellationToken);
        return _mapper.Map<IEnumerable<CouponResponseDto>>(coupons);
    }
}