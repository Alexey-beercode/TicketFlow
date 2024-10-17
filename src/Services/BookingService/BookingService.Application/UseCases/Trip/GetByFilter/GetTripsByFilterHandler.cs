using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using BookingService.Domain.Specifications.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetByFilter;

public class GetTripsByFilterHandler : IRequestHandler<GetTripsByFilterQuery, IEnumerable<TripResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITripFacade _tripFacade;

    public GetTripsByFilterHandler(IUnitOfWork unitOfWork, ITripFacade tripFacade)
    {
        _unitOfWork = unitOfWork;
        _tripFacade = tripFacade;
    }

    public async Task<IEnumerable<TripResponseDto>> Handle(GetTripsByFilterQuery request, CancellationToken cancellationToken)
    {
        var specification = new TripSearchSpecification(
            departureCity: request.DepartureCity,
            arrivalCity: request.ArrivalCity,
            departureDate: request.DepartureDate,
            arrivalDate: request.ArrivalDate,
            minPrice: request.MinPrice,
            maxPrice: request.MaxPrice,
            typeId: request.TypeId
        );

        var trips = await _unitOfWork.Trips.GetBySpecificationAsync(specification, cancellationToken);

        return await _tripFacade.GetListOfFullTripInfoAsync(trips);
    }
}