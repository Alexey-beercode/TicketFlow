using UserService.Domain.Entities;

namespace UserService.DLL.Repositories.Interfaces;

public interface IUserRepository:IBaseRepository<User>
{
    void Update(User entity);
    Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken=default);
}