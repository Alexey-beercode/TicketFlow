using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TicketStatusRepository:BaseRepository<TicketStatus>,ITicketStatusRepository
{
    public TicketStatusRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }
}