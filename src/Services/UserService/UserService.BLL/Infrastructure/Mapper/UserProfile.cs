using AutoMapper;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.User;
using UserService.Domain.Entities;

namespace UserService.BLL.Infrastructure.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<LoginDto, User>();
        CreateMap<RegisterUserDto, User>();
        CreateMap<RegisterUserDto, LoginDto>();

    }
}
