using System.Security.Claims;
using IdentityModel.Client;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Token;
using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.BLL.Interfaces;

public interface ITokenService
{
    Task<TokenDto> GenerateToken(LoginDto loginDto);
    Task<TokenRevocationResponse> RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}