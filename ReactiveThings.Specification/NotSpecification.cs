using System;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public class NotSpecification<TEntity> : Specification<TEntity>
    {
        private readonly ISpecification<TEntity> specification;

        public NotSpecification(ISpecification<TEntity> specification)
        {
            this.specification = specification;
        }
        public override Expression<Func<TEntity, bool>> Expression
        {
            get
            {
                return specification.Expression.Not();
            }
        }
    }
}