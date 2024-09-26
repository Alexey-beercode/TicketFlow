using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using System;

namespace UserService.DLL.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedUsersRolesData(this ModelBuilder modelBuilder)
        {
            var adminRoleId = new Guid("583E1840-BA88-418D-AE9E-4CE7571F0946");
            var adminId = new Guid("BD65E7BD-E25A-4935-81D1-05093B5F48C0");
            var adminRole = new Role
            {
                Id = adminRoleId,
                IsDeleted = false,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var hasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                Id = adminId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin14689"), 
                SecurityStamp = Guid.NewGuid().ToString(),
                IsDeleted = false,
                RefreshToken = "",
                RefreshTokenExpiryTime = DateTime.MinValue
            };
            
            modelBuilder.Entity<Role>().HasData(adminRole);
            modelBuilder.Entity<User>().HasData(adminUser);
            
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                UserId = adminId,
                RoleId = adminRoleId
            });
            
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Name = "Resident",
                NormalizedName = "RESIDENT"
            });
        }
    }
}
