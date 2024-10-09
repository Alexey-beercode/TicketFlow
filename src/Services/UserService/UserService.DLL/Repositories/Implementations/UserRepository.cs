using Microsoft.EntityFrameworkCore;
using UserService.DLL.Configuration;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Entities;

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

    public async Task<User> GetByNameAsync(string userName, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
    }
    public async Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => 
                    u.RefreshToken == refreshToken && 
                    !u.IsDeleted && 
                    u.RefreshTokenExpiryTime > DateTime.UtcNow.ToUniversalTime(),
                cancellationToken);
    }
    
    public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        user.IsDeleted = true;
        _dbSet.Update(user);
        
        await _dbContext.UserRoles
            .Where(userRole => userRole.UserId == user.Id && !userRole.IsDeleted)
            .ExecuteUpdateAsync(s => s.SetProperty(userRole => userRole.IsDeleted, true), cancellationToken);
    }
}