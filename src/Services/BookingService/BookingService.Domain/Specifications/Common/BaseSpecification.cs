using System.Linq.Expressions;
using BookingService.Domain.Interfaces.Specifications;

namespace BookingService.Domain.Specifications.Common;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
}