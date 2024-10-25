using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TicketStatusRepository:BaseRepository<TicketStatus>,ITicketStatusRepository
{
    public TicketStatusRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }

    public Task<TicketStatus> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return _dbSet.FirstOrDefaultAsync(status => status.Name == name && !status.IsDeleted,cancellationToken);
    }
}