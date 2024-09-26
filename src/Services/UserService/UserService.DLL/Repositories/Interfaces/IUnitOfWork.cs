namespace UserService.DLL.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
}