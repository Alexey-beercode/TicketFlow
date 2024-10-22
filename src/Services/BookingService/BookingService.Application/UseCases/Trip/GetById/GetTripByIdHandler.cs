using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.Exceptions;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Trip.GetById;

public class GetTripByIdHandler:IRequestHandler<GetTripByIdQuery,TripResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITripFacade _tripFacade;

    public GetTripByIdHandler(IUnitOfWork unitOfWork, ITripFacade tripFacade)
    {
        _unitOfWork = unitOfWork;
        _tripFacade = tripFacade;
    }

    public async Task<TripResponseDto> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
    {
        var trip = await _unitOfWork.Trips.GetByIdAsync(request.Id, cancellationToken);

        if (trip is null)
        {
            throw new EntityNotFoundException("Trip", request.Id);
        }

        return await _tripFacade.GetFullTripInfoAsync(trip,cancellationToken);
    }
}