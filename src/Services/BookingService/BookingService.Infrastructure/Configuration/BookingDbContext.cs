using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Configuration;

public class BookingDbContext:DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {
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
        
    }
}