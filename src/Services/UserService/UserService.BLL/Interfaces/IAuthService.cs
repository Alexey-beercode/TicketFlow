using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Auth;

namespace UserService.BLL.Interfaces;

public interface IAuthService
{
    Task LogoutAsync(string refreshToken,CancellationToken cancellationToken=default);
    Task ChangePasswordAsync(ChangePasswordDto changePasswordDto,CancellationToken cancellationToken=default);
    Task<AuthDto> AuthenticateAsync(LoginDto loginDto,CancellationToken cancellationToken=default);
    Task<AuthDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default);
    Task<AuthDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}