using System.Security.Claims;
using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.BLL.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, IEnumerable<Role> roles);
    TokenModel GenerateRefreshToken();
}