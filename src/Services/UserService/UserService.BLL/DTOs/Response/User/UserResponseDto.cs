using UserService.BLL.DTOs.Response.Role;

namespace UserService.BLL.DTOs.Response.User;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; }
}