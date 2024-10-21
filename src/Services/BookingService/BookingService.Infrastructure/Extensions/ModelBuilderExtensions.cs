using BookingService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedTripTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TripType>().HasData(
            new TripType { Id = Guid.NewGuid(), Name = "Airplane", IsDeleted = false },
            new TripType { Id = Guid.NewGuid(), Name = "Train", IsDeleted = false }
        );
    }

    public static void SeedDiscountTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiscountType>().HasData(
            new DiscountType { Id = Guid.NewGuid(), Name = "Percentage", IsDeleted = false },
            new DiscountType { Id = Guid.NewGuid(), Name = "Quantitative", IsDeleted = false }
        );
    }

    public static void SeedSeatTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SeatType>().HasData(
            new SeatType { Id = Guid.NewGuid(), Name = "Standard", PriceMultiplier = 1.0m, IsDeleted = false },
            new SeatType { Id = Guid.NewGuid(), Name = "Comfort", PriceMultiplier = 1.4m, IsDeleted = false },
            new SeatType { Id = Guid.NewGuid(), Name = "Business", PriceMultiplier = 2.0m, IsDeleted = false }
        );
    }

    public static void SeedTicketStatuses(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TicketStatus>().HasData(
            new TicketStatus { Id = Guid.NewGuid(), Name = "Booked", IsDeleted = false },
            new TicketStatus { Id = Guid.NewGuid(), Name = "Cancelled", IsDeleted = false },
            new TicketStatus { Id = Guid.NewGuid(), Name = "Completed", IsDeleted = false }
        );
    }

    public static void SeedAllData(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedTripTypes();
        modelBuilder.SeedDiscountTypes();
        modelBuilder.SeedSeatTypes();
        modelBuilder.SeedTicketStatuses();
    }
}