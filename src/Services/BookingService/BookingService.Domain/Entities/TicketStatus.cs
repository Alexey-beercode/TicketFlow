using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class TicketStatus:BaseEntity
{
    public string Name { get; set; }
}