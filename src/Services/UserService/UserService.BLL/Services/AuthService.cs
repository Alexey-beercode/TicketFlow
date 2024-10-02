using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Token;
using UserService.BLL.Exceptions;
using UserService.BLL.Interfaces;
using UserService.Domain.Entities;

namespace UserService.BLL.Services;

public class AuthService:IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(ILogger<AuthService> logger, IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    private void EnsureSuccessOrThrow(IdentityResult identityResult, string operation = "Operation")
    {
        if (!identityResult.Succeeded)
        {
            var errors = string.Join("\n", identityResult.Errors.Select(e => e.Description));
            throw new AuthorizationException($"{operation} failed: {errors}");
        }
    }
    
    private async Task<User> FindUserByNameOrThrowAsync(string name)
    {
        var user = await _userManager.FindByNameAsync(name);
        if (user==null )
        {
            throw new EntityNotFoundException($"User with name : {name} is not found");
        }

        return user;
    }

    private async Task CheckPasswordAsync(User user, string password)
    {
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
        if (!isPasswordCorrect)
        {
            throw new AuthorizationException("Password is incorrect");
        }
    }

    public async Task LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var revokeTokenResponse = await _tokenService.RevokeTokenAsync(refreshToken, cancellationToken);
        if (revokeTokenResponse.IsError) throw new AuthorizationException(revokeTokenResponse.Error);

        _logger.LogInformation("Successfully logged out");
    }

    public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken = default)
    {
        var user = await FindUserByNameOrThrowAsync(changePasswordDto.UserName);
        
        await CheckPasswordAsync(user, changePasswordDto.CurrentPassword);
        
        var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
        
        EnsureSuccessOrThrow(result, "Password change");
    }

    public async Task<TokenDto> AuthenticateAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        var user = await FindUserByNameOrThrowAsync(loginDto.UserName);
        
        await CheckPasswordAsync(user, loginDto.Password);
        var tokenDto = await _tokenService.GenerateToken(loginDto);
        return tokenDto;

    }

    public async Task<TokenDto> RegisterAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<User>(registerUserDto);
    
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);
        EnsureSuccessOrThrow(result, "User registration");
    
        return await AuthenticateAsync(_mapper.Map<LoginDto>(registerUserDto));
    }
}