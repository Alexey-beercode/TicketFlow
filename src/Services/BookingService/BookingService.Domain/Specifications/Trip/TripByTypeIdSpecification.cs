using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripByTypeIdSpecification : BaseSpecification<Entities.Trip>
{
    public TripByTypeIdSpecification(Guid typeId)
        : base(trip => trip.TypeId == typeId)
    {
    }
}