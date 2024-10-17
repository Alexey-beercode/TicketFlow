using AutoMapper;
using BookingService.Application.Exceptions;
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
        await ValidateEntitiesExistenceAsync(request, cancellationToken);

        var trip = await _unitOfWork.Trips.GetByIdAsync(request.TripId, cancellationToken);
        var seatType = await _unitOfWork.SeatTypes.GetByIdAsync(request.SeatTypeId, cancellationToken);
        var ticketStatus = await _unitOfWork.TicketStatuses.GetByNameAsync("Booked", cancellationToken);

        decimal discountValue = 0;
        if (!string.IsNullOrEmpty(request.CouponCode))
        {
            var coupon = await _unitOfWork.Coupons.GetByCodeAsync(request.CouponCode, cancellationToken);
            if (coupon != null)
            {
                await ValidateCoupon(coupon, request.UserId, cancellationToken);
                discountValue = await CalculateDiscountValue(coupon, trip.BasePrice, cancellationToken);
            }
        }

        var finalPrice = CalculateFinalPrice(trip.BasePrice, seatType.PriceMultiplier, discountValue);

        var ticket = _mapper.Map<Domain.Entities.Ticket>(request);
        ticket.Price = finalPrice;
        ticket.StatusId = ticketStatus.Id;

        await _unitOfWork.Tickets.CreateAsync(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateEntitiesExistenceAsync(CreateTicketCommand request, CancellationToken cancellationToken=default)
    {
        var entityChecks = new List<(string EntityName, Task<object> EntityTask)>
        {
            ("Trip", _unitOfWork.Trips.GetByIdAsync(request.TripId, cancellationToken).ContinueWith(t => (object)t.Result, cancellationToken)),
            ("TicketStatus", _unitOfWork.TicketStatuses.GetByIdAsync(request.StatusId, cancellationToken).ContinueWith(t => (object)t.Result, cancellationToken)),
            ("SeatType", _unitOfWork.SeatTypes.GetByIdAsync(request.SeatTypeId, cancellationToken).ContinueWith(t => (object)t.Result, cancellationToken))
        };

        await Task.WhenAll(entityChecks.Select(e => e.EntityTask));

        foreach (var (entityName, entityTask) in entityChecks)
        {
            var entity = await entityTask;
            if (entity == null)
            {
                throw new EntityNotFoundException($"{entityName} not found");
            }
        }
    }

    private async Task ValidateCoupon(Domain.Entities.Coupon coupon, Guid userId, CancellationToken cancellationToken)
    {
        if (coupon.IsPersonalized && !await _unitOfWork.Coupons.IsUserActiveCoupon(userId, coupon.Id, cancellationToken))
        {
            throw new CouponApplyException("User can't use this coupon");
        }

        if (coupon.UsedCount >= coupon.MaxUses || coupon.ValidUntil < DateTime.UtcNow)
        {
            throw new CouponApplyException("Coupon is invalid");
        }
    }

    private async Task<decimal> CalculateDiscountValue(Domain.Entities.Coupon coupon, decimal basePrice, CancellationToken cancellationToken)
    {
        var discountType = await _unitOfWork.DiscountTypes.GetByIdAsync(coupon.DiscountTypeId, cancellationToken);
        return discountType.Name switch
        {
            "Percentage" => basePrice * coupon.DiscountValue / 100,
            "Quantitative" => coupon.DiscountValue,
            _ => 0
        };
    }

    private static decimal CalculateFinalPrice(decimal basePrice, decimal priceMultiplier, decimal discount)
    {
        return basePrice * priceMultiplier - discount;
    }
}