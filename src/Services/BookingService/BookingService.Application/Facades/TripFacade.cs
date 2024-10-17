using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.DTOs.Response.Trip;
using BookingService.Application.DTOs.Response.TripSeatAvailability;
using BookingService.Application.DTOs.Response.TripType;
using BookingService.Application.Exceptions;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.UnitOfWork;

namespace BookingService.Application.Facades
{
    public class TripFacade : ITripFacade
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TripFacade(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TripResponseDto> GetFullTripInfoAsync(Trip trip,CancellationToken cancellationToken=default)
        {
            var tripDto = _mapper.Map<TripResponseDto>(trip);

            var tripType = await _unitOfWork.TripTypes.GetByIdAsync(trip.TypeId);
            
            if (tripType is null) 
            {
                throw new EntityNotFoundException("Trip type", trip.TypeId);
            }
            
            tripDto.Type = _mapper.Map<TripTypeResponseDto>(tripType);
            
            var seatAvailabilities = await _unitOfWork.TripSeatAvailabilities.GetByTripIdAsync(trip.Id);
            tripDto.SeatsAvailability = new List<TripSeatAvailabilityDto>();

            foreach (var seatAvailability in seatAvailabilities)
            {
                var seatAvailabilityDto = _mapper.Map<TripSeatAvailabilityDto>(seatAvailability);
                var seatType = await _unitOfWork.SeatTypes.GetByIdAsync(seatAvailability.SeatTypeId);

                if (seatType is null) 
                {
                    throw new EntityNotFoundException("Seat type", seatAvailability.SeatTypeId);
                }

                seatAvailabilityDto.SeatType = _mapper.Map<SeatTypeResponseDto>(seatType);
                tripDto.SeatsAvailability.Add(seatAvailabilityDto);

            }
            
            return tripDto;
        }

        public async Task<IEnumerable<TripResponseDto>> GetListOfFullTripInfoAsync(IEnumerable<Trip> trips, CancellationToken cancellationToken = default)
        {
            var tripResponseDtos = new List<TripResponseDto>();

            foreach (var trip in trips)
            {
                var ticketDto = await GetFullTripInfoAsync(trip, cancellationToken);
                tripResponseDtos.Add(ticketDto);
            }

            return tripResponseDtos;
        }
    }
}