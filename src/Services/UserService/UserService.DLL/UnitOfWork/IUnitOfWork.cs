using UserService.DLL.Repositories.Interfaces;

namespace UserService.DLL.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}