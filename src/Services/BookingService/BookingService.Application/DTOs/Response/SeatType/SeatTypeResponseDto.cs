using BookingService.Application.DTOs.BaseDtos;

namespace BookingService.Application.DTOs.Response.SeatType;

public class SeatTypeResponseDto:BaseTypeDto
{
    public double PriceMultiplier { get; set; }
}