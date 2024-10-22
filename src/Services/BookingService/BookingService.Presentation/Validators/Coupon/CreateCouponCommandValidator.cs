using BookingService.Application.UseCases.Coupon.Create;
using FluentValidation;

namespace BookingService.Presentation.Validators.Coupon;

public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code cannot be empty")
            .MaximumLength(50).WithMessage("Coupon code cannot be longer than 50 characters");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0");

        RuleFor(x => x.MaxUses)
            .GreaterThanOrEqualTo(0).WithMessage("Maximum uses cannot be negative");

        RuleFor(x => x.ValidUntil)
            .GreaterThan(DateTime.Now).WithMessage("Valid until date must be in the future");

        RuleFor(x => x.DiscountTypeId)
            .NotEmpty().WithMessage("Discount type ID must be specified");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID must be specified");
    }
}