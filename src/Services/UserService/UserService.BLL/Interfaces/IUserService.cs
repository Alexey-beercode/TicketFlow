using UserService.BLL.DTOs.Response.User;

namespace UserService.BLL.Interfaces;

public interface IUserService
{
   Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
   Task<UserDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
   Task<UserDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}