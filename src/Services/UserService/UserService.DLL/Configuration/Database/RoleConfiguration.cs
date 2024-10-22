﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.DLL.Configuration.Database
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.IsDeleted)
                .IsRequired();

            builder.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}