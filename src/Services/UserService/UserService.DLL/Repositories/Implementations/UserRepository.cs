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
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
    }
}