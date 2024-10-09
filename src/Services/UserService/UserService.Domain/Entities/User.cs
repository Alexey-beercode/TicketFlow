using Microsoft.AspNetCore.Identity;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Entities;

public class User:BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}