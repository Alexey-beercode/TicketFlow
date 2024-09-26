using Microsoft.EntityFrameworkCore;
using UserService.DLL.Configuration;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Entities.Implementations;

namespace UserService.DLL.Repositories.Implementations;

public class UserRepository:BaseRepository<User>,IUserRepository
{
    private readonly UserDbContext _dbContext;
    public UserRepository(UserDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(User entity)
    {
        _dbContext.Users.Update(entity);
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => !u.IsDeleted && u.RefreshToken == refreshToken);
    }
}