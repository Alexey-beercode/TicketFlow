using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;

namespace UserService.BLL.Infrastructure.Identity;

public class IdentityServerConfig
{
    private readonly IConfiguration _configuration;

    public IdentityServerConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = _configuration["IdentityServer:ClientId"],
                ClientSecrets = { new Secret(_configuration["IdentityServer:ClientSecret"].Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = { _configuration["IdentityServer:Scope"], "openid", "profile" }, 
                AccessTokenLifetime = int.Parse(_configuration["IdentityServer:AccessTokenLifetime"]),
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = int.Parse(_configuration["IdentityServer:RefreshTokenLifetime"]),
                SlidingRefreshTokenLifetime = int.Parse(_configuration["IdentityServer:SlidingRefreshTokenLifetime"]),
            }
        };
    }

    public IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope(_configuration["IdentityServer:Scope"], "TicketFlow API")
        };
    }

    public IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
    
}