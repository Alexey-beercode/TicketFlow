using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.Interfaces;
using UserService.Domain.Helpers;

namespace UserService.Domain.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController:ControllerBase
{
    private readonly IAuthService _authService;
    private readonly LoggerHelper<AuthController> _loggerHelper;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _loggerHelper = new LoggerHelper<AuthController>(logger);
    }

   
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto,
        CancellationToken cancellationToken = default)
    {
       _loggerHelper.LogStartRequest("registration", paramName: "username", paramValue: registerUserDto.Username);
        
        var tokenDto = await _authService.RegisterAsync(registerUserDto, cancellationToken);
        
       _loggerHelper.LogEndOfOperation("registration","return tokens");
        return Ok(tokenDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest("login", paramName: "username", paramValue: loginDto.UserName);
        
        var tokens = await _authService.AuthenticateAsync(loginDto, cancellationToken);
        
        _loggerHelper.LogEndOfOperation("login","return tokens");
        return Ok(tokens);
    }

    [Authorize]
    [HttpPut("logout")]
    public async Task<IActionResult> LogoutAsync([FromBody] string refreshToken,
        CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest("logout");
        
        await _authService.LogoutAsync(refreshToken, cancellationToken);
        
        _loggerHelper.LogEndOfOperation("logging out","return ok response");
        return Ok();
    }

    [Authorize]
    [HttpPut("changePassword")]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePasswordDto,
        CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest("change password","username", changePasswordDto.UserName);
        
        await _authService.ChangePasswordAsync(changePasswordDto, cancellationToken);
        
        _loggerHelper.LogEndOfOperation("changing password","return ok response");
        return Ok();
    }
}