using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface IDiscountTypeRepository:IBaseRepository<DiscountType>
{
    Task DeleteAsync(DiscountType discountType, CancellationToken cancellationToken = default);
}