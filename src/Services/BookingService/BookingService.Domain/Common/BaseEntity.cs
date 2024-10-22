using BookingService.Domain.Interfaces.Entities;

namespace BookingService.Domain.Common;

public class BaseEntity:IHasId,ISoftDeletable
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}