using UserService.Domain.Interfaces;

namespace UserService.Domain.Entities;

public class BaseEntity:IHasId,ISoftDeletable
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}