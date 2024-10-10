using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Repositories.Implementations
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(BookingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tickets
                .AsNoTracking()
                .Where(t => t.UserId == userId && !t.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetByStatusIdAsync(Guid statusId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tickets
                .AsNoTracking()
                .Where(t => t.StatusId == statusId && !t.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetByTripIdAsync(Guid tripId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tickets
                .AsNoTracking()
                .Where(t => t.TripId == tripId && !t.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetByCouponIdAsync(Guid couponId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tickets
                .AsNoTracking()
                .Where(t => t.CouponId == couponId && !t.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Ticket>> GetBySeatTypeIdAsync(Guid seatTypeId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tickets
                .AsNoTracking()
                .Where(t => t.SeatTypeId == seatTypeId && !t.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        

        public void Delete(Ticket ticket)
        {
            ticket.IsDeleted = true;
            _dbContext.Tickets.Update(ticket);
            
        }
    }
}