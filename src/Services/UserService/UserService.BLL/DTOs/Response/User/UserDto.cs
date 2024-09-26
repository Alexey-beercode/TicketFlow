using UserService.BLL.DTOs.Response.Role;

namespace UserService.BLL.DTOs.Response.User;

public class UserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<RoleDto> Roles { get; set; }
}