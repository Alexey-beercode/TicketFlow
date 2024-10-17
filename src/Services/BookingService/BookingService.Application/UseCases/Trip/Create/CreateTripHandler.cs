using AutoMapper;
using BookingService.Application.DTOs.Request.TripSeatAvailability;
using BookingService.Application.Exceptions;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.UnitOfWork;
using BookingService.Domain.Specifications.Trip;
using MediatR;

namespace BookingService.Application.UseCases.Trip.Create;

public class CreateTripHandler : IRequestHandler<CreateTripCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTripHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        await ValidateTripType(request.TypeId, cancellationToken);

        var trip = _mapper.Map<Domain.Entities.Trip>(request);
        await _unitOfWork.Trips.CreateAsync(trip, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var newTrip = await GetCreatedTrip(trip, cancellationToken);

        await CreateTripSeatAvailabilities(request.TripSeatAvailabilityRequestDtos, newTrip.Id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateTripType(Guid typeId, CancellationToken cancellationToken)
    {
        var type = await _unitOfWork.TripTypes.GetByIdAsync(typeId, cancellationToken);
        if (type is null)
        {
            throw new EntityNotFoundException("Trip type", typeId);
        }
    }

    private async Task<Domain.Entities.Trip> GetCreatedTrip(Domain.Entities.Trip trip, CancellationToken cancellationToken)
    {
        var specification = new TripSearchSpecification(
            departureCity: trip.DepartureCity,
            arrivalCity: trip.ArrivalCity,
            departureDate: trip.DepartureTime,
            arrivalDate: trip.ArrivalTime,
            typeId: trip.TypeId
        );

        var createdTrip = (await _unitOfWork.Trips.GetBySpecificationAsync(specification, cancellationToken)).FirstOrDefault();
        if (createdTrip is null)
        {
            throw new EntityNotFoundException("Created trip not found");
        }
        
        return createdTrip;
    }

    private async Task CreateTripSeatAvailabilities(IEnumerable<TripSeatAvailabilityRequestDto> seatAvailabilityDtos, Guid tripId, CancellationToken cancellationToken)
    {
        foreach (var seatAvailabilityDto in seatAvailabilityDtos)
        {
            await ValidateSeatType(seatAvailabilityDto.SeatTypeId, cancellationToken);

            var seatAvailability = _mapper.Map<TripSeatAvailability>(seatAvailabilityDto);
            seatAvailability.TripId = tripId;
            await _unitOfWork.TripSeatAvailabilities.CreateAsync(seatAvailability, cancellationToken);
        }
    }

    private async Task ValidateSeatType(Guid seatTypeId, CancellationToken cancellationToken)
    {
        var seatType = await _unitOfWork.SeatTypes.GetByIdAsync(seatTypeId, cancellationToken);
        if (seatType is null)
        {
            throw new EntityNotFoundException("Seat type", seatTypeId);
        }
    }
}