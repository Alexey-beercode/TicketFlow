using Microsoft.EntityFrameworkCore;
using UserService.DLL.Configuration;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Interfaces;

namespace UserService.DLL.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T: class,IHasId,ISoftDeletable
{
    protected readonly UserDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().Where(e => !e.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
    
}