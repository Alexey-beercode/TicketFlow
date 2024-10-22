using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.Exceptions;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using BookingService.Domain.Specifications.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetByTypeId;

public class GetTripsByTypeIdHandler : IRequestHandler<GetTripsByTypeIdQuery, IEnumerable<TripResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITripFacade _tripFacade;

    public GetTripsByTypeIdHandler(IUnitOfWork unitOfWork, ITripFacade tripFacade)
    {
        _unitOfWork = unitOfWork;
        _tripFacade = tripFacade;
    }

    public async Task<IEnumerable<TripResponseDto>> Handle(GetTripsByTypeIdQuery request, CancellationToken cancellationToken)
    {
        var type = await _unitOfWork.TripTypes.GetByIdAsync(request.TypeId, cancellationToken);

        if (type is null)
        {
            throw new EntityNotFoundException("Trip type", request.TypeId);
        }
        
        var specification = new TripByTypeIdSpecification(request.TypeId);
        var trips = await _unitOfWork.Trips.GetBySpecificationAsync(specification, cancellationToken);

        return await _tripFacade.GetListOfFullTripInfoAsync(trips);
    }
}