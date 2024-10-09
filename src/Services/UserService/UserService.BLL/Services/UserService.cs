using AutoMapper;
using UserService.BLL.DTOs.Response.Role;
using UserService.BLL.DTOs.Response.User;
using UserService.BLL.Exceptions;
using UserService.BLL.Interfaces;
using UserService.DLL.Repositories.Interfaces;
using UserService.DLL.UnitOfWork;
using UserService.Domain.Entities;

namespace UserService.BLL.Services;

public class UserService:IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private async Task<UserResponseDto> CreateUserDtoWithRolesAsync(User user, CancellationToken cancellationToken = default)
    {
        var userDto = _mapper.Map<UserResponseDto>(user);
        
        var rolesByUser = await _unitOfWork.Roles.GetRolesByUserIdAsync(user.Id, cancellationToken);
        userDto.Roles = _mapper.Map<IEnumerable<RoleDto>>(rolesByUser);
        
        return userDto;
    }
    
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var usersFromDb = await _unitOfWork.Users.GetAllAsync(cancellationToken);
        var usersResponses = new List<UserResponseDto>();
        foreach (var user in usersFromDb)
        {
            var userResponseDto = await CreateUserDtoWithRolesAsync(user,cancellationToken);
            usersResponses.Add(userResponseDto);
        }

        return usersResponses;
    }

    public async Task<UserResponseDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException("User", userId);
        }
        
        var userResponseDto = await CreateUserDtoWithRolesAsync(user,cancellationToken);
        return userResponseDto;
    }

    public async Task<UserResponseDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByNameAsync(userName, cancellationToken);
        if (user is null)
        {
            throw new EntityNotFoundException($"User with username : {userName} are not found");
        }
        
        var userResponseDto = await CreateUserDtoWithRolesAsync(user,cancellationToken);
        return userResponseDto; 
    }
}