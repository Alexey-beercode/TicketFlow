namespace UserService.Domain.Models;

public class TokenModel
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExireTime { get; set; }
}