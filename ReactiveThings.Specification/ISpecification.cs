using System;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Expression { get; }

        bool IsSatisfiedBy(TEntity entity);
    }
}