using AutoMapper;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.Token;
using UserService.BLL.Interfaces;

namespace UserService.BLL.Services;

public class TokenService:ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IMapper mapper, ILogger<TokenService> logger)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<TokenDto> GenerateToken(LoginDto loginDto)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync(_configuration["IdentityServer:Authority"]);

        if (disco.IsError) throw new Exception(disco.Error);

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = _configuration["IdentityServer:ClientId"],
            ClientSecret = _configuration["IdentityServer:ClientSecret"],
            Scope = _configuration["IdentityServer:Scope"],
            Password = loginDto.Password,
            UserName = loginDto.UserName
        });

        if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);

        _logger.LogInformation("Successful authorization by user with name : {LoginDtoUserName}", loginDto.UserName);
        
        return _mapper.Map<TokenDto>(tokenResponse);
    }
    public async Task<TokenRevocationResponse> RevokeTokenAsync(string refreshToken,CancellationToken cancellationToken=default)
    {
        var client = _httpClientFactory.CreateClient();
        var disco = await client.GetDiscoveryDocumentAsync(_configuration["IdentityServer:Authority"]);

        if (disco.IsError) throw new Exception(disco.Error);

        var revokeTokenResponse = await client.RevokeTokenAsync(new TokenRevocationRequest
        {
            Address = disco.RevocationEndpoint,
            ClientId = _configuration["IdentityServer:ClientId"],
            ClientSecret = _configuration["IdentityServer:ClientSecret"],
            Token = refreshToken,
            TokenTypeHint = "refresh_token"
        });

        return revokeTokenResponse;
    }
}