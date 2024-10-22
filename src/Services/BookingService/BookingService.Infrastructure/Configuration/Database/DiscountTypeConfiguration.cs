using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
{
    public void Configure(EntityTypeBuilder<DiscountType> builder)
    {
        builder.ToTable("DiscountTypes");

        builder.HasKey(dt => dt.Id);

        builder.Property(dt => dt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(dt => dt.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(dt => dt.Name)
            .IsUnique();
    }
}