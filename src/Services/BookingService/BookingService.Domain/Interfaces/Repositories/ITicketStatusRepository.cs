﻿using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ITicketStatusRepository:IBaseRepository<TicketStatus>
{
    Task<TicketStatus> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}