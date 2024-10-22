using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripByDepartureDateSpecification : BaseSpecification<Entities.Trip>
{
    public TripByDepartureDateSpecification(DateTime departureDate)
        : base(trip => trip.DepartureTime.Date == departureDate.Date)
    {
    }
}