using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Coupon.GetUsedByUser;

public class GetUsedByUserHandler:IRequestHandler<GetUsedByUserQuery,IEnumerable<CouponResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUsedByUserHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CouponResponseDto>> Handle(GetUsedByUserQuery request, CancellationToken cancellationToken)
    {
        var coupons = await _unitOfWork.Coupons.GetUsedByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<CouponResponseDto>>(coupons);
    }
}