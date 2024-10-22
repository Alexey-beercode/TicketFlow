using AutoMapper;
using BookingService.Application.DTOs.Response.TicketStatus;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class TicketStatusProfile:Profile
{
    public TicketStatusProfile()
    {
        CreateMap<TicketStatus, TicketStatusResponseDto>();
    }
}