using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.SeatType.GetAll;

public class GetAllHandler:IRequestHandler<GetAllQuery,IEnumerable<SeatTypeResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllHandler(IMapper mapper, ILogger<GetAllHandler> logger, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SeatTypeResponseDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all seat types");
        
        var seatTypes = await _unitOfWork.SeatTypes.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<SeatTypeResponseDto>>(seatTypes);
    }
}