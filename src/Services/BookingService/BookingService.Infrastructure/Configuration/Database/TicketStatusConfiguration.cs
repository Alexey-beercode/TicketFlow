using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
{
    public void Configure(EntityTypeBuilder<TicketStatus> builder)
    {
        builder.ToTable("TicketStatuses");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(ts => ts.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(ts => ts.Name)
            .IsUnique();
    }
}