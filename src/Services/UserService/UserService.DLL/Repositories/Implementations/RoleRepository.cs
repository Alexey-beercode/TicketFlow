using UserService.DLL.Configuration;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Entities;

namespace UserService.DLL.Repositories.Implementations;

public class RoleRepository:BaseRepository<Role>,IRoleRepository
{
    private readonly UserDbContext _dbContext;
    public RoleRepository(UserDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}