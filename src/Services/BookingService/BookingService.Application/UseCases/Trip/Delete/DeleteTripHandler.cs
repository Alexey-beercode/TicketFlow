using BookingService.Application.Exceptions;
using BookingService.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace BookingService.Application.UseCases.Trip.Delete;

public class DeleteTripHandler:IRequestHandler<DeleteTripCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTripHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _unitOfWork.Trips.GetByIdAsync(request.Id, cancellationToken);

        if (trip is null)
        {
            throw new EntityNotFoundException("Trip", request.Id);
        }

        await _unitOfWork.Trips.DeleteAsync(trip, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}