using System;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public class ExpressionSpecification<TEntity> : Specification<TEntity>
    {
        public override Expression<Func<TEntity, bool>> Expression { get; }

        public ExpressionSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Expression = expression;
        }
    }
}