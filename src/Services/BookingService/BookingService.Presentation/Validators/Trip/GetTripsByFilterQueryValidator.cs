using BookingService.Application.UseCases.Trip.GetByFilter;
using FluentValidation;

namespace BookingService.Presentation.Validators.Trip;

public class GetTripsByFilterQueryValidator : AbstractValidator<GetTripsByFilterQuery>
{
    public GetTripsByFilterQueryValidator()
    {
        RuleFor(x => x.DepartureCity)
            .MaximumLength(100).WithMessage("Departure city name cannot be longer than 100 characters");

        RuleFor(x => x.ArrivalCity)
            .MaximumLength(100).WithMessage("Arrival city name cannot be longer than 100 characters");

        RuleFor(x => x.DepartureDate)
            .GreaterThanOrEqualTo(DateTime.Today).When(x => x.DepartureDate.HasValue)
            .WithMessage("Departure date cannot be in the past");

        RuleFor(x => x.ArrivalDate)
            .GreaterThanOrEqualTo(x => x.DepartureDate).When(x => x.ArrivalDate.HasValue && x.DepartureDate.HasValue)
            .WithMessage("Arrival date must be later than or equal to departure date");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
            .WithMessage("Minimum price cannot be negative");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(x => x.MinPrice).When(x => x.MaxPrice.HasValue && x.MinPrice.HasValue)
            .WithMessage("Maximum price must be greater than or equal to minimum price");
    }
}