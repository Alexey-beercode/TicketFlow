using AutoMapper;
using BookingService.Application.Exceptions;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Ticket.Create;

public class CreateTicketHandler : IRequestHandler<CreateTicketCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTicketHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var (trip, seatType, seatAvailability, ticketStatus) = await GetAndValidateEntitiesAsync(request, cancellationToken);

        var (couponId, discountValue) = await ProcessCouponAsync(request, trip.BasePrice, cancellationToken);

        var finalPrice = trip.BasePrice * seatType.PriceMultiplier - discountValue;
        
        await CreateTicket(request, ticketStatus.Id, couponId, finalPrice);
        
        await UpdateSeatAndCouponAsync(seatAvailability, couponId, cancellationToken);
    }
    
    private async Task<(Domain.Entities.Trip trip, Domain.Entities.SeatType seatType, TripSeatAvailability
            seatAvailability, Domain.Entities.TicketStatus ticketStatus)>
        GetAndValidateEntitiesAsync(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var trip = await _unitOfWork.Trips.GetByIdAsync(request.TripId, cancellationToken)
                   ?? throw new EntityNotFoundException("Trip not found");

        var seatType = await _unitOfWork.SeatTypes.GetByIdAsync(request.SeatTypeId, cancellationToken)
                       ?? throw new EntityNotFoundException("SeatType not found");

        var seatAvailability =
            await _unitOfWork.TripSeatAvailabilities.GetAvailableByTripIdAndSeatTypeIdAsync(request.TripId,
                request.SeatTypeId, cancellationToken)
            ?? throw new InvalidOperationException("No available seats for this seat type on this trip");

        var ticketStatus = await _unitOfWork.TicketStatuses.GetByNameAsync("Booked", cancellationToken);

        return (trip, seatType, seatAvailability, ticketStatus);
    }
    
    private async Task<(Guid? couponId, decimal discountValue)> ProcessCouponAsync(CreateTicketCommand request, decimal basePrice, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.CouponCode))
            return (null, 0);

        var coupon = await _unitOfWork.Coupons.GetByCodeAsync(request.CouponCode, cancellationToken)
                      ?? throw new CouponApplyException("Coupon not found");

        await ValidateCouponAsync(coupon, request.UserId, cancellationToken);

        var discountValue = await CalculateDiscountValueAsync(coupon, basePrice, cancellationToken);

        return (coupon.Id, discountValue);
    }
    
    private async Task ValidateCouponAsync(Domain.Entities.Coupon coupon, Guid userId, CancellationToken cancellationToken)
    {
        if (coupon.IsPersonalized && !await _unitOfWork.Coupons.IsUserActiveCoupon(userId, coupon.Id, cancellationToken))
            throw new CouponApplyException("User cannot use this coupon");

        if (coupon.UsedCount >= coupon.MaxUses || coupon.ValidUntil < DateTime.UtcNow)
            throw new CouponApplyException("Coupon is invalid or expired");
    }
    
    private async Task<decimal> CalculateDiscountValueAsync(Domain.Entities.Coupon coupon, decimal basePrice, CancellationToken cancellationToken)
    {
        var discountType = await _unitOfWork.DiscountTypes.GetByIdAsync(coupon.DiscountTypeId, cancellationToken);
        return discountType.Name switch
        {
            "Percentage" => basePrice * coupon.DiscountValue / 100,
            "Quantitative" => coupon.DiscountValue,
            _ => 0
        };
    }
    
    private async Task CreateTicket(CreateTicketCommand request, Guid statusId, Guid? couponId, decimal finalPrice)
    {
        var ticket = _mapper.Map<Domain.Entities.Ticket>(request);
        ticket.Price = finalPrice;
        ticket.StatusId = statusId;
        ticket.CouponId = couponId;
        
        await _unitOfWork.Tickets.CreateAsync(ticket);
    }
    
    private async Task UpdateSeatAndCouponAsync(TripSeatAvailability seatAvailability, Guid? couponId, CancellationToken cancellationToken)
    {
        seatAvailability.SeatsAvailable--;
        _unitOfWork.TripSeatAvailabilities.Update(seatAvailability);

        if (couponId.HasValue)
        {
            var coupon = await _unitOfWork.Coupons.GetByIdAsync(couponId.Value, cancellationToken);
            coupon.UsedCount++;
            _unitOfWork.Coupons.Update(coupon);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}