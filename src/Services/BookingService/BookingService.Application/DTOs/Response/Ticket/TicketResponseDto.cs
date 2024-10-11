using BookingService.Application.DTOs.BaseDtos;
using BookingService.Application.DTOs.Response.Coupon;
using BookingService.Application.DTOs.Response.SeatType;
using BookingService.Domain.Entities;

namespace BookingService.Application.DTOs.Response.Ticket;

public class TicketResponseDto:BaseDto
{
    public decimal Price { get; set; }
    public Guid UserId { get; set; }
    public Guid TripId { get; set; }
    public TicketResponseDto Status { get; set; }
    public CouponResponseDto Coupon { get; set; }
    public SeatTypeResponseDto SeatType { get; set; }
}