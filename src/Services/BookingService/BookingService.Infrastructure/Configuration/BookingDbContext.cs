using BookingService.Domain.Entities;
using BookingService.Infrastructure.Configuration.Database;
using BookingService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Configuration;

public class BookingDbContext:DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
    
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<DiscountType> DiscountTypes { get; set; }
    public DbSet<SeatType> SeatTypes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripSeatAvailability> TripSeatAvailabilities { get; set; }
    public DbSet<TripType> TripTypes { get; set; }
    public DbSet<UserCoupon> UsersCoupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CouponConfiguration());
        modelBuilder.ApplyConfiguration(new DiscountTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SeatTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new TicketStatusConfiguration());
        modelBuilder.ApplyConfiguration(new TripConfiguration());
        modelBuilder.ApplyConfiguration(new TripSeatAvailabilityConfiguration());
        modelBuilder.ApplyConfiguration(new TripTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserCouponConfiguration());
        
        modelBuilder.SeedAllData();
    }
}