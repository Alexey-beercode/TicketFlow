using AutoMapper;
using BookingService.Application.DTOs.Response.Ticket;
using BookingService.Application.UseCases.Ticket.Create;
using BookingService.Domain.Entities;

namespace BookingService.Application.Mappers;

public class TicketProfile:Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketResponseDto>();
        CreateMap<CreateTicketCommand, Ticket>();
    }
}