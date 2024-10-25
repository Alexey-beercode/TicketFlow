using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class TripTypeConfiguration : IEntityTypeConfiguration<TripType>
{
    public void Configure(EntityTypeBuilder<TripType> builder)
    {
        builder.ToTable("TripTypes");

        builder.HasKey(tt => tt.Id);

        builder.Property(tt => tt.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(tt => tt.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(tt => tt.Name)
            .IsUnique();
    }
}