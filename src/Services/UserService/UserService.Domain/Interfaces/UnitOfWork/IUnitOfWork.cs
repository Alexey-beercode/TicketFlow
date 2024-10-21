using UserService.Domain.Interfaces.Repositories;

namespace UserService.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}