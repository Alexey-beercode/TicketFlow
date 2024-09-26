using UserService.Domain.Entities;
using UserService.Domain.Entities.Implementations;

namespace UserService.DLL.Repositories.Interfaces;

public interface IUserRepository:IBaseRepository<User>
{
    void Update(User entity);
    Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken=default);
}