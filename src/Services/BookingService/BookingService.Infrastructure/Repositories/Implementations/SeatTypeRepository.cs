using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class SeatTypeRepository:BaseRepository<SeatType>,ISeatTypeRepository
{
    public SeatTypeRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }
}