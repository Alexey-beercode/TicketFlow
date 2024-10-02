using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Token;

namespace UserService.BLL.Interfaces;

public interface IAuthService
{
    Task LogoutAsync(string refreshToken,CancellationToken cancellationToken=default);
    Task ChangePasswordAsync(ChangePasswordDto changePasswordDto,CancellationToken cancellationToken=default);
    Task<TokenDto> AuthenticateAsync(LoginDto loginDto,CancellationToken cancellationToken=default);
    Task<TokenDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default);
}