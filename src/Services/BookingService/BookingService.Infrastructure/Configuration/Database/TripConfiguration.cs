using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.DepartureCity)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.ArrivalCity)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.DepartureTime)
            .IsRequired();

        builder.Property(t => t.ArrivalTime)
            .IsRequired();

        builder.Property(t => t.BasePrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<TripType>()
            .WithMany()
            .HasForeignKey(t => t.TypeId);

        builder.HasIndex(t => new { t.DepartureCity, t.ArrivalCity, t.DepartureTime });
    }
}