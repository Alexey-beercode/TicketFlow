using BookingService.Domain.Interfaces;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Interfaces.UnitOfWork;
using BookingService.Infrastructure.Configuration;

namespace BookingService.Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private bool _disposed;
    private readonly BookingDbContext _dbContext;
    private readonly ICouponRepository _couponRepository;
    private readonly IDiscountTypeRepository _discountTypeRepository;
    private readonly ISeatTypeRepository _seatTypeRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketStatusRepository _ticketStatusRepository;
    private readonly ITripRepository _tripRepository;
    private readonly ITripSeatAvailabilityRepository _seatAvailabilityRepository;
    private readonly ITripTypeRepository _tripTypeRepository;

    public UnitOfWork(BookingDbContext dbContext, 
        ICouponRepository couponRepository, 
        IDiscountTypeRepository discountTypeRepository, 
        ISeatTypeRepository seatTypeRepository, 
        ITicketRepository ticketRepository, 
        ITicketStatusRepository ticketStatusRepository, 
        ITripRepository tripRepository, 
        ITripSeatAvailabilityRepository seatAvailabilityRepository, 
        ITripTypeRepository tripTypeRepository)
    {
        _dbContext = dbContext;
        _couponRepository = couponRepository;
        _discountTypeRepository = discountTypeRepository;
        _seatTypeRepository = seatTypeRepository;
        _ticketRepository = ticketRepository;
        _ticketStatusRepository = ticketStatusRepository;
        _tripRepository = tripRepository;
        _seatAvailabilityRepository = seatAvailabilityRepository;
        _tripTypeRepository = tripTypeRepository;
    }

    public ICouponRepository Coupons => _couponRepository;
    public IDiscountTypeRepository DiscountTypes => _discountTypeRepository;
    public ISeatTypeRepository SeatTypes => _seatTypeRepository;
    public ITicketRepository Tickets => _ticketRepository;
    public ITicketStatusRepository TicketStatuses => _ticketStatusRepository;
    public ITripRepository Trips => _tripRepository;
    public ITripSeatAvailabilityRepository TripSeatAvailabilities => _seatAvailabilityRepository;
    public ITripTypeRepository TripTypes => _tripTypeRepository;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}