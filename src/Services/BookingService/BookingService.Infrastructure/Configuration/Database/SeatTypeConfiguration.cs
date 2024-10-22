using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class SeatTypeConfiguration : IEntityTypeConfiguration<SeatType>
{
    public void Configure(EntityTypeBuilder<SeatType> builder)
    {
        builder.ToTable("SeatTypes");

        builder.HasKey(st => st.Id);

        builder.Property(st => st.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(st => st.PriceMultiplier)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(st => st.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(st => st.Name)
            .IsUnique();
    }
}