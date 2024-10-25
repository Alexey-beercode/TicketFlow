namespace UserService.BLL.DTOs.Response.Auth;

public class AuthDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
}