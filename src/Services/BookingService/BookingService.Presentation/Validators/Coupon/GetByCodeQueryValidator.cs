using BookingService.Application.UseCases.Coupon.GetByCode;
using FluentValidation;

namespace BookingService.Presentation.Validators.Coupon;

public class GetByCodeQueryValidator : AbstractValidator<GetByCodeQuery>
{
    public GetByCodeQueryValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Coupon code cannot be empty")
            .MaximumLength(50).WithMessage("Coupon code cannot be longer than 50 characters");
    }
}