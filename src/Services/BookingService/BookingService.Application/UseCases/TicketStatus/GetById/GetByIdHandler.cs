using AutoMapper;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.TicketStatus.GetById;

public class GetByIdHandler:IRequestHandler<GetByIdQuery,TicketStatusResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketStatusResponseDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var ticketStatus = await _unitOfWork.TicketStatuses.GetByIdAsync(request.Id, cancellationToken);
        
        if (ticketStatus is null)
        {
            throw new EntityNotFoundException("TicketStatus", request.Id);
        }

        return _mapper.Map<TicketStatusResponseDto>(ticketStatus);
    }
}