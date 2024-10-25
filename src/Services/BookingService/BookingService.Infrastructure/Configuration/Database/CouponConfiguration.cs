using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.DiscountValue)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.IsPersonalized)
            .IsRequired();

        builder.Property(c => c.MaxUses)
            .IsRequired();

        builder.Property(c => c.UsedCount)
            .IsRequired();

        builder.Property(c => c.ValidUntil)
            .IsRequired();

        builder.Property(c => c.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<DiscountType>()
            .WithMany()
            .HasForeignKey(c => c.DiscountTypeId);

        builder.HasIndex(c => c.Code)
            .IsUnique();
    } 
}