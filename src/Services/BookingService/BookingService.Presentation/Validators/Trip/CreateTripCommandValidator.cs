using BookingService.Application.UseCases.Trip.Create;
using FluentValidation;

namespace BookingService.Presentation.Validators.Trip;

public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(x => x.DepartureCity)
            .NotEmpty().WithMessage("Departure city cannot be empty")
            .MaximumLength(100).WithMessage("Departure city name cannot be longer than 100 characters");

        RuleFor(x => x.ArrivalCity)
            .NotEmpty().WithMessage("Arrival city cannot be empty")
            .MaximumLength(100).WithMessage("Arrival city name cannot be longer than 100 characters");

        RuleFor(x => x.DepartureTime)
            .GreaterThan(DateTime.Now).WithMessage("Departure time must be in the future");

        RuleFor(x => x.ArrivalTime)
            .GreaterThan(x => x.DepartureTime).WithMessage("Arrival time must be later than departure time");

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Base price must be greater than 0");

        RuleFor(x => x.TypeId)
            .NotEmpty().WithMessage("Trip type ID must be specified");

        RuleFor(x => x.TripSeatAvailabilityRequestDtos)
            .NotEmpty().WithMessage("At least one seat availability must be specified")
            .ForEach(seat => {
                seat.ChildRules(seatRule => {
                    seatRule.RuleFor(s => s.SeatTypeId).NotEmpty().WithMessage("Seat type ID must be specified");
                    seatRule.RuleFor(s => s.SeatsAvailable).GreaterThan(0).WithMessage("Seat quantity must be greater than 0");
                });
            });
    }
}