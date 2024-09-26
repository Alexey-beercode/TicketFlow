using UserService.BLL.DTOs.Response.Role;

namespace UserService.BLL.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken=default);
}