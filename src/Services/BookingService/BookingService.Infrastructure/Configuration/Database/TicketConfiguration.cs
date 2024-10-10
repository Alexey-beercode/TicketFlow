using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(t => t.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<Trip>()
            .WithMany()
            .HasForeignKey(t => t.TripId);

        builder.HasOne<TicketStatus>()
            .WithMany()
            .HasForeignKey(t => t.StatusId);

        builder.HasOne<Coupon>()
            .WithMany()
            .HasForeignKey(t => t.CouponId);

        builder.HasOne<SeatType>()
            .WithMany()
            .HasForeignKey(t => t.SeatTypeId);

        builder.HasIndex(t => new { t.UserId, t.TripId });
    }
}