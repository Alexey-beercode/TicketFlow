using AutoMapper;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Application.Interfaces.Facades;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.UnitOfWork;

namespace BookingService.Application.Facades;

public class TicketFacade: ITicketFacade
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TicketFacade(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketResponseDto> GetFullTicketInfoAsync(Ticket ticket,CancellationToken cancellationToken=default)
    {
        var ticketDto=_mapper.Map<TicketResponseDto>(ticket);
        
        var ticketStatus = await _unitOfWork.TicketStatuses.GetByIdAsync(ticket.StatusId,cancellationToken);
        var ticketStatusDto = _mapper.Map<TicketStatusResponseDto>(ticketStatus);

        var couponByTicket = await _unitOfWork.Coupons.GetByIdAsync(ticket.CouponId,cancellationToken);
        
        if (couponByTicket is not null)
        {
            ticketDto.Coupon = _mapper.Map<CouponResponseDto>(couponByTicket);
        }

        var seatTypeByTicket = await _unitOfWork.SeatTypes.GetByIdAsync(ticket.SeatTypeId,cancellationToken);
        var seatTypeDto = _mapper.Map<SeatTypeResponseDto>(seatTypeByTicket);

        ticketDto.SeatType = seatTypeDto;
        ticketDto.Status = ticketStatusDto;

        return ticketDto;
    }

    public async Task<List<TicketResponseDto>> GetListOfFullTicketInfoAsync(IEnumerable<Ticket> tickets,
        CancellationToken cancellationToken = default)
    {
        var ticketsDtos = new List<TicketResponseDto>();

        foreach (var ticket in tickets)
        {
            var ticketDto = await GetFullTicketInfoAsync(ticket, cancellationToken);
            ticketsDtos.Add(ticketDto);
        }

        return ticketsDtos;
    }
}