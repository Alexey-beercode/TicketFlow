namespace BookingService.Domain.Interfaces.Entities;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}
