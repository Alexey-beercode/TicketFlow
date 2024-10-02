using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using UserService.DLL.Repositories.Interfaces;
using UserService.Domain.Entities;

namespace UserService.BLL.Infrastructure.Identity
{
    public class UserProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserProfileService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.FindFirst(JwtClaimTypes.Subject)?.Value;
            if (userId != null)
            {
                var user = await _unitOfWork.Users.GetByIdAsync(Guid.Parse(userId));
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                        new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                        new Claim(JwtClaimTypes.Name, user.UserName),
                        new Claim(JwtClaimTypes.Email, user.Email),
                    };
                        var rolesByUser = await _userManager.GetRolesAsync(user);
                        foreach (var role in rolesByUser)
                        {
                            claims.Add(new Claim(JwtClaimTypes.Role, role));
                        }

                    context.IssuedClaims.AddRange(claims);
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _unitOfWork.Users.GetByIdAsync(Guid.Parse(userId));
                context.IsActive = user != null;
            }
        }
    }
}