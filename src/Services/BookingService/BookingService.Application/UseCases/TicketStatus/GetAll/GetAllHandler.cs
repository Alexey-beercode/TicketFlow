using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.TicketStatus.GetAll;

public class GetAllHandler:IRequestHandler<GetAllQuery,IEnumerable<TicketResponseDto>>
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

    public async Task<IEnumerable<TicketResponseDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all ticket statuses");
        
        var ticketStatuses = await _unitOfWork.TicketStatuses.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TicketResponseDto>>(ticketStatuses);
    }
}