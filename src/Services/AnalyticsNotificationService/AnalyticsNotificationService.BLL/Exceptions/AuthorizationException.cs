namespace BookingService.Application.Exceptions;

public class AuthorizationException:Exception
{
    public AuthorizationException(string meassage):base(meassage){}
}