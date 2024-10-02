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
                AllowedScopes = { _configuration["IdentityServer:Scope"] },
                AccessTokenLifetime = 3600, 
                AllowOfflineAccess = true,  
                RefreshTokenUsage = TokenUsage.OneTimeOnly, 
                RefreshTokenExpiration = TokenExpiration.Sliding, 
                AbsoluteRefreshTokenLifetime = 2592000, 
                SlidingRefreshTokenLifetime = 1296000,
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