using AutoMapper;
using Microsoft.Extensions.Logging;
using UserService.BLL.DTOs.Response.User;
using UserService.BLL.Exceptions;
using UserService.BLL.Interfaces;
using UserService.DLL.Repositories.Interfaces;

namespace UserService.BLL.Services;

public class UserService:IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var usersFromDb = await _unitOfWork.Users.GetAllAsync(cancellationToken);
        return usersFromDb.Select(user => _mapper.Map<UserDto>(user)).ToList();
    }

    public async Task<UserDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var userFromDb = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (userFromDb==null)
        {
            throw new EntityNotFoundException("User", userId);
        }
        return _mapper.Map<UserDto>(userFromDb);
    }

    public async Task<UserDto> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var userFromDb = await _unitOfWork.Users.GetByNameAsync(userName, cancellationToken);
        if (userFromDb==null)
        {
            throw new EntityNotFoundException($"User with username : {userName} are not found");
        }
        return _mapper.Map<UserDto>(userFromDb);
    }
}