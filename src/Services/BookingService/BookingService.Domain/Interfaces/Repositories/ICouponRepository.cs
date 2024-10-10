using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<IEnumerable<Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Coupon>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Coupon>> GetUsedByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Coupon> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Coupon>> GetByDiscountTypeIdAsync(Guid discountTypeId,
        CancellationToken cancellationToken = default);
    Task DeleteAsync(Coupon coupon, CancellationToken cancellationToken);
}