namespace UserService.Domain.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
