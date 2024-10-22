using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.BLL.Interfaces;
using UserService.Domain.Helpers;

namespace UserService.Domain.Areas.Admin.Controllers;

[Route("api/admin/role")]
[ApiController]
[Authorize(Policy = "Admin")]
public class RoleController:ControllerBase
{
    private readonly LoggerHelper<RoleController> _loggerHelper;
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService,ILogger<RoleController> logger)
    {
        _roleService = roleService;
        _loggerHelper = new LoggerHelper<RoleController>(logger);
    }

    [HttpGet("getById{roleId}")]
    public async Task<IActionResult> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest("get role by id","role id",roleId.ToString());
        var role = await _roleService.GetByIdAsync(roleId, cancellationToken);
        
        _loggerHelper.LogEndOfOperation("getting role by id","return role");
        return Ok(role);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _loggerHelper.LogStartRequest("get all roles");
        var roles = await _roleService.GetAllAsync(cancellationToken);
        
        _loggerHelper.LogEndOfOperation("getting all roles","return all roles");
        return Ok(roles);
    }
}