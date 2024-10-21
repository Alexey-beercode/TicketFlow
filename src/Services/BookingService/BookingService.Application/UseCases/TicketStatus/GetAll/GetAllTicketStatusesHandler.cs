using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingService.Application.UseCases.TicketStatus.GetAll;

public class GetAllTicketStatusesHandler:IRequestHandler<GetAllTicketStatusesQuery,IEnumerable<TicketStatusResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllTicketStatusesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTicketStatusesHandler(IMapper mapper, ILogger<GetAllTicketStatusesHandler> logger, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TicketStatusResponseDto>> Handle(GetAllTicketStatusesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all ticket statuses");
        
        var ticketStatuses = await _unitOfWork.TicketStatuses.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TicketStatusResponseDto>>(ticketStatuses);
    }
}