using System.Linq.Expressions;

namespace BookingService.Domain.Interfaces.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}