using AutoMapper;
using UserService.BLL.DTOs.Request.User;
using UserService.BLL.DTOs.Response.User;
using UserService.Domain.Entities;

namespace UserService.BLL.Infrastructure.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<LoginDto, User>();
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}
