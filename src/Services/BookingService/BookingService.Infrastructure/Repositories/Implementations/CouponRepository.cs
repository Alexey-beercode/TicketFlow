using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations
{
    public class CouponRepository :BaseRepository<Coupon>,ICouponRepository
    {
        private readonly BookingDbContext _dbContext;

        public CouponRepository(BookingDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Coupons.AsNoTracking()
                .Where(c => !c.IsDeleted && c.ValidUntil > DateTime.UtcNow && c.UsedCount < c.MaxUses)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Coupon>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.UsersCoupons.AsNoTracking()
                .Where(uc => uc.UserId == userId && !uc.IsDeleted)
                .Join(_dbContext.Coupons,
                    uc => uc.CounponId,
                    c => c.Id,
                    (uc, c) => c)
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Coupon>> GetUsedByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.UsersCoupons.AsNoTracking()
                .Where(uc => uc.UserId == userId && uc.IsUsed && !uc.IsDeleted)
                .Join(_dbContext.Coupons,
                    uc => uc.CounponId,
                    c => c.Id,
                    (uc, c) => c)
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Coupon> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Coupons.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Code == code && !c.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<Coupon>> GetByDiscountTypeIdAsync(Guid discountTypeId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Coupons.AsNoTracking()
                .Where(c => c.DiscountTypeId == discountTypeId && !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(Coupon coupon, CancellationToken cancellationToken)
        {
            coupon.IsDeleted = true;
            _dbContext.Coupons.Update(coupon);
            
            await _dbContext.UsersCoupons
                .Where(userCoupon => userCoupon.CounponId ==coupon.Id && !userCoupon.IsDeleted)
                .ExecuteUpdateAsync(s => s.SetProperty(userCoupon => userCoupon.IsDeleted, true), cancellationToken);
        }

        public async Task AddCouponToUserAsync(UserCoupon userCoupon, CancellationToken cancellationToken = default)
        {
            await _dbContext.UsersCoupons.AddAsync(userCoupon, cancellationToken);
        }

        public Task<bool> IsUserActiveCoupon(Guid userId, Guid couponId, CancellationToken cancellationToken = default)
        {
            return _dbContext.UsersCoupons.AnyAsync(uc =>
                uc.UserId == userId && uc.CounponId == couponId && !uc.IsUsed && !uc.IsDeleted);
        }
    }
}