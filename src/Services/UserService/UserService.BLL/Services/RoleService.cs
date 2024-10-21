using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTOs.Response.Role;
using UserService.BLL.Exceptions;
using UserService.BLL.Interfaces;
using UserService.DLL.UnitOfWork;
using UserService.Domain.Interfaces.UnitOfWork;

namespace UserService.BLL.Services;

public class RoleService:IRoleService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleService> _logger;

    public RoleService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<RoleService> logger)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var rolesFromDb = await _unitOfWork.Roles.GetAllAsync(cancellationToken);
    
        var rolesDtos = _mapper.Map<IEnumerable<RoleDto>>(rolesFromDb);
    
        _logger.LogInformation("Roles have been obtained");
    
        return rolesDtos;
    }

    public async Task<RoleDto> GetByIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(roleId, cancellationToken);
        if (role is null)
        {
            throw new EntityNotFoundException("Role", roleId);
        }
        
        _logger.LogInformation("Role with id : {Id} has been obtained from db", roleId);
        return _mapper.Map<RoleDto>(role);
    }
}