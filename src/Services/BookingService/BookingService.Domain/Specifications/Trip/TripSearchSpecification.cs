using System.Linq.Expressions;
using BookingService.Domain.Specifications.Common;

namespace BookingService.Domain.Specifications.Trip;

public class TripSearchSpecification : BaseSpecification<Entities.Trip>
{
    public TripSearchSpecification(
        string departureCity = null,
        string arrivalCity = null,
        DateTime? departureDate = null,
        DateTime? arrivalDate = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        Guid? typeId = null)
        : base(BuildCriteria(departureCity, arrivalCity, departureDate, arrivalDate, minPrice, maxPrice, typeId))
    {
    }

    private static Expression<Func<Entities.Trip, bool>> BuildCriteria(
        string departureCity,
        string arrivalCity,
        DateTime? departureDate,
        DateTime? arrivalDate,
        decimal? minPrice,
        decimal? maxPrice,
        Guid? typeId)
    {
        return trip =>
            (string.IsNullOrEmpty(departureCity) || trip.DepartureCity.ToLower().Contains(departureCity.ToLower())) &&
            (string.IsNullOrEmpty(arrivalCity) || trip.ArrivalCity.ToLower().Contains(arrivalCity.ToLower())) &&
            (!departureDate.HasValue || trip.DepartureTime.Date == departureDate.Value.Date) &&
            (!arrivalDate.HasValue || trip.ArrivalTime.Date == arrivalDate.Value.Date) &&
            (!minPrice.HasValue || trip.BasePrice >= minPrice.Value) &&
            (!maxPrice.HasValue || trip.BasePrice <= maxPrice.Value) &&
            (!typeId.HasValue || trip.TypeId == typeId.Value);
    }
}