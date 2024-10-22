using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Infrastructure.Configuration;

namespace BookingService.Infrastructure.Repositories.Implementations;

public class TripTypeRepository:BaseRepository<TripType>,ITripTypeRepository
{
    public TripTypeRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }
}