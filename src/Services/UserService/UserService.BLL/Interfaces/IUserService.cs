using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Token;

namespace UserService.BLL.Interfaces;

public interface IUserService
{
    Task LogoutAsync(Guid userId,CancellationToken cancellationToken=default);
    Task ChangePasswordAsync(ChangePasswordDto dto,CancellationToken cancellationToken=default);
    Task<TokenDto> RefreshTokenAsync(string refreshToken,CancellationToken cancellationToken=default);
    Task<TokenDto> AuthenticateAsync(LoginDto loginDto,CancellationToken cancellationToken=default);
    Task<TokenDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default);
}