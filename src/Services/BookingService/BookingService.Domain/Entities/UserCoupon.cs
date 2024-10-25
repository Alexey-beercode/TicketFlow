using BookingService.Domain.Common;

namespace BookingService.Domain.Entities;

public class UserCoupon:BaseEntity
{
    public bool IsUsed { get; set; }
    public Guid UserId { get; set; }
    public Guid CounponId { get; set; }
}