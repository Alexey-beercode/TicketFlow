using UserService.DLL.Configuration;
using UserService.DLL.Repositories.Interfaces;

namespace UserService.DLL.Repositories.Implementations;

public class UnitOfWork:IUnitOfWork
{
    private readonly UserDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public IUserRepository Users => _userRepository;
    public IRoleRepository Roles => _roleRepository;

    public UnitOfWork(UserDbContext dbContext, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }
 
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}