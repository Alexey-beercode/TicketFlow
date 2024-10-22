using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.SeatType.GetById;

public class GetByIdHandler:IRequestHandler<GetByIdQuery,SeatTypeResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SeatTypeResponseDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var seatType = await _unitOfWork.SeatTypes.GetByIdAsync(request.Id, cancellationToken);
        
        if (seatType is null)
        {
            throw new EntityNotFoundException("SeatType", request.Id);
        }

        return _mapper.Map<SeatTypeResponseDto>(seatType);
    }
}