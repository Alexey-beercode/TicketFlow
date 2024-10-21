﻿using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces.Repositories;

public interface IUserRepository:IBaseRepository<User>
{
    void Update(User entity);
    Task<User> GetByNameAsync(string userName, CancellationToken cancellationToken=default);
    Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}