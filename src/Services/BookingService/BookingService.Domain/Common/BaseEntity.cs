using BookingService.Domain.Interfaces.Entities;
using UserService.Domain.Interfaces;

namespace BookingService.Domain.Common;

public class BaseEntity:IHasId,ISoftDeletable
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}