using AutoMapper;
using IdentityModel.Client;
using UserService.BLL.DTOs.Response.Token;

namespace UserService.BLL.Infrastructure.Mapper;

public class TokenProfile:Profile
{
    public TokenProfile()
    {
        CreateMap<TokenResponse, TokenDto>();
    }
}