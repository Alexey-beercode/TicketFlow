using AutoMapper;
using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.DTOs.Response.TripType;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.TripType.GetAll;

public class GetAllHandler:IRequestHandler<GetAllQuery,IEnumerable<TripTypeResponseDto>>
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

    public async Task<IEnumerable<TripTypeResponseDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all trip types");
        
        var tripTypes = await _unitOfWork.TripTypes.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TripTypeResponseDto>>(tripTypes);
    }
}