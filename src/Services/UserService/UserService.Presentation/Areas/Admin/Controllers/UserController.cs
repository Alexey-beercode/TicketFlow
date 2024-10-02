using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.BLL.Interfaces;
using UserService.Domain.Helpers;

namespace UserService.Domain.Areas.Admin.Controllers;

[Route("api/admin/user")]
[ApiController]
[Authorize(Policy = "Admin")]
public class UserController:ControllerBase
{
    private readonly IUserService _userService;
    private readonly LoggerHelper<UserController> _loggerHelper;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _loggerHelper = new LoggerHelper<UserController>(logger);
    }

    [HttpGet("getById{userid}")]
    public async Task<IActionResult> GetByIdAsync(Guid userid, CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest( "get user by user id");
        var user = await _userService.GetByIdAsync(userid, cancellationToken);
        
        _loggerHelper.LogEndOfOperation("getting user by id","return user dto");
        return Ok(user);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        _loggerHelper.LogStartRequest("get all users");
        var users = await _userService.GetAllAsync(cancellationToken);
        
        _loggerHelper.LogEndOfOperation("getting all users","return all users");
        return Ok(users);
    }

    [HttpGet("getByName/{userName}")]
    public async Task<IActionResult> GetByName(string userName, CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest("get user by username","username", userName);
        var user = await _userService.GetByUserNameAsync(userName, cancellationToken);
        
        _loggerHelper.LogEndOfOperation("getting user by username","return user");
        return Ok(user);
    }
}