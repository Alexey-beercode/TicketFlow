using AnalyticsNotificationService.BLL.DTOs.Request.Notification;
using FluentValidation;

namespace AnalyticsNotificationService.BLL.Validators;

public class CreateNotificationDtoValidator : AbstractValidator<CreateNotificationDto>
{
    public CreateNotificationDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Message is required")
            .MaximumLength(500)
            .WithMessage("Message cannot be longer than 500 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(100)
            .WithMessage("Email cannot be longer than 100 characters");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(2)
            .WithMessage("Username must be at least 2 characters long")
            .MaximumLength(50)
            .WithMessage("Username cannot be longer than 50 characters");
    }
}