using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripSearchSpecification : BaseSpecification<Entities.Trip>
{
    public TripSearchSpecification(string departureCity, string arrivalCity, DateTime departureDate)
        : base(trip => 
            trip.DepartureCity.ToLower() == departureCity.ToLower() &&
            trip.ArrivalCity.ToLower() == arrivalCity.ToLower() &&
            trip.DepartureTime.Date == departureDate.Date)
    {
    }
}