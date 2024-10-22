using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetAll;

public class GetAllTripsHandler:IRequestHandler<GetAllTripsQuery,IEnumerable<TripResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITripFacade _tripFacade;

    public GetAllTripsHandler(IUnitOfWork unitOfWork, ITripFacade tripFacade)
    {
        _unitOfWork = unitOfWork;
        _tripFacade = tripFacade;
    }

    public async Task<IEnumerable<TripResponseDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
    {
        var trips = await _unitOfWork.Trips.GetAllAsync(cancellationToken);
        return await _tripFacade.GetListOfFullTripInfoAsync(trips);
    }
}