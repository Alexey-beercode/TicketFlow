using UserService.BLL.DTOs.Response.User;

namespace UserService.BLL.Interfaces;

public interface IUserService
{
   Task<IEnumerable<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
   Task<UserResponseDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
   Task<UserResponseDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}