using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;

namespace UserService.BLL.Infrastructure.Identity;

public class ResourceOwnerPasswordValidator<TUser> : IResourceOwnerPasswordValidator
    where TUser : User
{
    private readonly UserManager<TUser> _userManager;

    public ResourceOwnerPasswordValidator(UserManager<TUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByNameAsync(context.UserName);
        if (user != null)
        {
            var result = await _userManager.CheckPasswordAsync(user, context.Password);
            if (result)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), "password");
                return;
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password.");
            return;
        }

        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
    }
}