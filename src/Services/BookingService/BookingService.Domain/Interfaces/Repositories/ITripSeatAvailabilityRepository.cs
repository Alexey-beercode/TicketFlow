﻿using BookingService.Domain.Entities;

namespace BookingService.Domain.Interfaces.Repositories;

public interface ITripSeatAvailabilityRepository:IBaseRepository<TripSeatAvailability>
{
    Task DeleteAsync(Coupon coupon, CancellationToken cancellationToken);
}