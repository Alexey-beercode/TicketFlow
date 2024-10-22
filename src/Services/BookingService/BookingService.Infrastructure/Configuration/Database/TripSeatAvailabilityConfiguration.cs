using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class TripSeatAvailabilityConfiguration : IEntityTypeConfiguration<TripSeatAvailability>
{
    public void Configure(EntityTypeBuilder<TripSeatAvailability> builder)
    {
        builder.ToTable("TripSeatAvailabilities");

        builder.HasKey(tsa => tsa.Id);

        builder.Property(tsa => tsa.SeatsAvailable)
            .IsRequired();

        builder.Property(tsa => tsa.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<Trip>()
            .WithMany()
            .HasForeignKey(tsa => tsa.TripId);

        builder.HasOne<SeatType>()
            .WithMany()
            .HasForeignKey(tsa => tsa.SeatTypeId);

        builder.HasIndex(tsa => new { tsa.TripId, tsa.SeatTypeId })
            .IsUnique();
    }
}