using BookingService.Application.UseCases.Ticket.Create;
using FluentValidation;

namespace BookingService.Presentation.Validators.Ticket;

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID must be specified");

        RuleFor(x => x.TripId)
            .NotEmpty().WithMessage("Trip ID must be specified");

        RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("Status ID must be specified");

        RuleFor(x => x.CouponCode)
            .MaximumLength(50).WithMessage("Coupon code cannot be longer than 50 characters");

        RuleFor(x => x.SeatTypeId)
            .NotEmpty().WithMessage("Seat type ID must be specified");
    }
}