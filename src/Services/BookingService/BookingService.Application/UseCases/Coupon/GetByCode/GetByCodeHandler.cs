using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.GetByCode;

public class GetByCodeHandler:IRequestHandler<GetByCodeQuery,CouponResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetByCodeHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetByCodeHandler(IMapper mapper, ILogger<GetByCodeHandler> logger, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CouponResponseDto> Handle(GetByCodeQuery request, CancellationToken cancellationToken)
    {
        var couponByCode = await _unitOfWork.Coupons.GetByCodeAsync(request.Code, cancellationToken);
        _logger.LogInformation("Getting coupon by code");

        if (couponByCode is null)
        {
            throw new EntityNotFoundException($"Coupon with code : {request.Code} not found");
        }

        return _mapper.Map<CouponResponseDto>(couponByCode);
    }
    
}