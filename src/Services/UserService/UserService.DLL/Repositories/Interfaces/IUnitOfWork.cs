namespace UserService.DLL.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}