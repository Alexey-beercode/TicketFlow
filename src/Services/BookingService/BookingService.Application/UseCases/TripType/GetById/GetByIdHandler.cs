using AutoMapper;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.DTOs.Response.TripType;
using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.TripType.GetById;

public class GetByIdHandler:IRequestHandler<GetByIdQuery,TripTypeResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TripTypeResponseDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var tripType = await _unitOfWork.TripTypes.GetByIdAsync(request.Id, cancellationToken);
        
        if (tripType is null)
        {
            throw new EntityNotFoundException("TripType", request.Id);
        }

        return _mapper.Map<TripTypeResponseDto>(tripType);
    }
}