using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.DLL.Configuration.Database;
using UserService.DLL.Extensions;
using UserService.Domain.Entities;

namespace UserService.DLL.Configuration;

public class UserDbContext:IdentityDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.SeedUsersRolesData();
    }
}