using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripByPriceRangeSpecification : BaseSpecification<Entities.Trip>
{
    public TripByPriceRangeSpecification(decimal minPrice, decimal maxPrice)
        : base(trip => trip.BasePrice >= minPrice && trip.BasePrice <= maxPrice)
    {
    }
}