namespace BookingService.Application.Exceptions;

public class CouponApplyException:Exception
{
    public CouponApplyException(string message) : base(message)
    { }   
}