using System.Collections;
using BookingService.Application.DTOs.Response.Trip;
using BookingService.Domain.Entities;

namespace BookingService.Application.Interfaces.Facades;

public interface ITripFacade
{
    Task<TripResponseDto> GetFullTripInfoAsync(Trip trip,CancellationToken cancellationToken=default);
    Task<IEnumerable<TripResponseDto>> GetListOfFullTripInfoAsync(IEnumerable<Trip> trips,CancellationToken cancellationToken=default);
}