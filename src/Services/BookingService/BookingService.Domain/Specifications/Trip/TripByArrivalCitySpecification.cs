using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripByArrivalCitySpecification : BaseSpecification<Entities.Trip>
{
    public TripByArrivalCitySpecification(string arrivalCity)
        : base(trip => trip.ArrivalCity.ToLower() == arrivalCity.ToLower())
    {
    }
}