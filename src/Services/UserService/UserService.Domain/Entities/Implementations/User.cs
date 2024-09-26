using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities.Interfaces;

namespace UserService.Domain.Entities.Implementations;

public class User:IdentityUser<Guid>,ISoftDeletable,IHasId
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}