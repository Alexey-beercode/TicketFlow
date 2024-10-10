using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Infrastructure.Configuration.Database;

public class UserCouponConfiguration : IEntityTypeConfiguration<UserCoupon>
{
    public void Configure(EntityTypeBuilder<UserCoupon> builder)
    {
        builder.ToTable("UserCoupons");

        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.IsUsed)
            .IsRequired();

        builder.Property(uc => uc.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne<Coupon>()
            .WithMany()
            .HasForeignKey(uc => uc.CounponId);

        builder.HasIndex(uc => new { uc.UserId, uc.CounponId })
            .IsUnique();
    }
}