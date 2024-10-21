using AutoMapper;
using UserService.BLL.DTOs.Response.Role;
using UserService.Domain.Entities;

namespace UserService.BLL.Infrastructure.Mapper;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
    }
}