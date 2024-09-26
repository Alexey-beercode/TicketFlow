using System.Security.Claims;
using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.BLL.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    RefreshTokenModel GenerateRefreshToken();
    List<Claim> CreateClaims(User user, List<Role> roles);
}