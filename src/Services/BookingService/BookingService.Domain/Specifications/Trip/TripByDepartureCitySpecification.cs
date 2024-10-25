using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripByDepartureCitySpecification : BaseSpecification<Entities.Trip>
{
    public TripByDepartureCitySpecification(string departureCity)
        : base(trip => trip.DepartureCity.ToLower() == departureCity.ToLower())
    {
    }
}