using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Auth;
using UserService.BLL.Exceptions;
using UserService.BLL.Helpers;
using UserService.BLL.Interfaces;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Entities;

namespace UserService.BLL.Services;

public class AuthService:IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        ILogger<AuthService> logger, 
        IMapper mapper, 
        ITokenService tokenService, 
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    
    private async Task<User> FindUserByNameOrThrowAsync(string name,CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(name, cancellationToken);
        if (user==null )
        {
            throw new EntityNotFoundException($"User with name : {name} is not found");
        }

        return user;
    }

    private async Task CheckPasswordAsync(string oldPassword, string newPassword)
    {
        var isPasswordCorrect = PasswordHelper.VerifyPassword(oldPassword, newPassword);
        if (!isPasswordCorrect)
        {
            throw new AuthorizationException("Password is incorrect");
        }
    }

    public async Task LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByRefreshTokenAsync(refreshToken, cancellationToken);

        if (user==null)
        {
            throw new EntityNotFoundException("User not found");
        }

        user.RefreshToken = string.Empty;
        user.RefreshTokenExpiryTime = DateTime.MinValue;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully logged out");
    }

    public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken = default)
    {
        var user = await FindUserByNameOrThrowAsync(changePasswordDto.UserName,cancellationToken);
        
        await CheckPasswordAsync(user.PasswordHash, changePasswordDto.CurrentPassword);

        user.PasswordHash = PasswordHelper.HashPassword(changePasswordDto.NewPassword);
        
        _logger.LogInformation("Successful changed password");
    }

    public async Task<AuthDto> AuthenticateAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        var user = await FindUserByNameOrThrowAsync(loginDto.UserName,cancellationToken);
        
        await CheckPasswordAsync(user.PasswordHash, loginDto.Password);

        var roles = await _unitOfWork.Roles.GetRolesByUserIdAsync(user.Id, cancellationToken);
        
        var accessToken = _tokenService.GenerateAccessToken(user,roles);
        var refreshTokenModel = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshTokenModel.RefreshToken;
        user.RefreshTokenExpiryTime = refreshTokenModel.RefreshTokenExireTime;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new AuthDto(){RefreshToken = refreshTokenModel.RefreshToken,AccessToken = accessToken,UserId = user.Id};

    }

    public async Task<AuthDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default)
    {
        var userFromDb = await _unitOfWork.Users.GetByNameAsync(registerUserDto.Username, cancellationToken);
        if (userFromDb != null)
        {
            throw new AlreadyExistsException("user", "username", registerUserDto.Username);
        }
        var user = _mapper.Map<User>(registerUserDto);
        user.PasswordHash = PasswordHelper.HashPassword(registerUserDto.Password);
        
        await _unitOfWork.Users.CreateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation( "Successful user registration");

        var residentRole = await _unitOfWork.Roles.GetByNameAsync("Resident", cancellationToken);
        
        await _unitOfWork.Roles.SetRoleToUserAsync(user.Id, residentRole.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return await AuthenticateAsync(_mapper.Map<LoginDto>(registerUserDto));
    }

    public async Task<AuthDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByRefreshTokenAsync(refreshToken, cancellationToken);
        if (user==null)
        {
            throw new EntityNotFoundException("User not found");
        }

        var rolesByUser = await _unitOfWork.Roles.GetRolesByUserIdAsync(user.Id, cancellationToken);
        
        var accessToken = _tokenService.GenerateAccessToken(user,rolesByUser);
        var refreshTokenModel = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshTokenModel.RefreshToken;
        user.RefreshTokenExpiryTime = refreshTokenModel.RefreshTokenExireTime;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthDto() { RefreshToken = refreshToken, AccessToken = accessToken,UserId = user.Id};
    }
}