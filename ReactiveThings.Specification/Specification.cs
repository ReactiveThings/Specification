using System;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public abstract class Specification<TEntity> : ISpecification<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> Expression { get; }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}