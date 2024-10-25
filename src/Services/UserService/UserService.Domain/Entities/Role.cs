using Microsoft.AspNetCore.Identity;
using UserService.Domain.Interfaces;

namespace UserService.Domain.Entities;

public class Role:BaseEntity
{
    public string Name { get; set; }
}