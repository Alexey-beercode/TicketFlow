﻿

using UserService.Domain.Entities.Interfaces;

namespace UserService.DLL.Repositories.Interfaces;

public interface IBaseRepository<T> where T:IHasId,ISoftDeletable
{
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    void Delete(T entity);
}