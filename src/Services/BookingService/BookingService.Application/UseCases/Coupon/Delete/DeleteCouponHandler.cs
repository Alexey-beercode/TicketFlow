using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.Coupon.Delete;

public class DeleteCouponHandler:IRequestHandler<DeleteCouponCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCouponHandler> _logger;

    public DeleteCouponHandler(IUnitOfWork unitOfWork, ILogger<DeleteCouponHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await _unitOfWork.Coupons.GetByIdAsync(request.Id);

        if (coupon is null)
        {
            throw new EntityNotFoundException("Coupon", request.Id);
        }

        _logger.LogInformation("Deleting coupon with id : {Id}",request.Id);
        await _unitOfWork.Coupons.DeleteAsync(coupon, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}