using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class DiscountTypeRepository:BaseRepository<DiscountType>,IDiscountTypeRepository
{
    public DiscountTypeRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }

    public async Task DeleteAsync(DiscountType discountType, CancellationToken cancellationToken = default)
    {
        discountType.IsDeleted = true;
        _dbContext.DiscountTypes.Update(discountType);
        
        await _dbContext.Coupons
            .Where(coupon => coupon.DiscountTypeId ==discountType.Id && !coupon.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(coupon => coupon.IsDeleted, true), cancellationToken);
    }
}