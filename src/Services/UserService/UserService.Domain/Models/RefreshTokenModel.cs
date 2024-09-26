namespace UserService.Domain.Models;

public class RefreshTokenModel
{
    public string RefreshToken { get; set; }
    public DateTime ExpireTime { get; set; }
}