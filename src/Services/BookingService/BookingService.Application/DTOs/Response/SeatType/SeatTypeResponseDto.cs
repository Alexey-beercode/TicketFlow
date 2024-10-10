namespace BookingService.Application.DTOs.Response.SeatType;

public class SeatTypeResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double PriceMultiplier { get; set; }
}