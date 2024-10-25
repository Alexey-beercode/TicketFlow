using AutoMapper;
using BookingService.Application.Exceptions;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.Create;

public class CreateCouponHandler:IRequestHandler<CreateCouponCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCouponHandler> _logger;
    private readonly IMapper _mapper;

    public CreateCouponHandler(IUnitOfWork unitOfWork, ILogger<CreateCouponHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking for coupon with the same code");
        
        var couponByCode = await _unitOfWork.Coupons.GetByCodeAsync(request.Code, cancellationToken);

        if (couponByCode is not null)
        {
            throw new AlreadyExistsException("Coupon", "code", request.Code);
        }

        var discountType = await _unitOfWork.DiscountTypes.GetByIdAsync(request.DiscountTypeId, cancellationToken);

        if (discountType is null)
        {
            throw new EntityNotFoundException("DiscountType", request.DiscountTypeId);
        }
        
        var coupon = _mapper.Map<Domain.Entities.Coupon>(request);
        
        await _unitOfWork.Coupons.CreateAsync(coupon, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (request.IsPersonalized)
        {
            _logger.LogInformation("Adding coupon to user");
            
            var newCoupon = await _unitOfWork.Coupons.GetByCodeAsync(request.Code);
            var userCoupon = new UserCoupon() { CounponId = newCoupon.Id, UserId = request.UserId };

            await _unitOfWork.Coupons.AddCouponToUserAsync(userCoupon, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}