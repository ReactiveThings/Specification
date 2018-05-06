using System;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public class OrSpecification<TEntity> : Specification<TEntity>
    {
        private readonly ISpecification<TEntity> specification1;
        private readonly ISpecification<TEntity> specification2;

        public OrSpecification(ISpecification<TEntity> specification1, ISpecification<TEntity> specification2)
        {
            this.specification1 = specification1;
            this.specification2 = specification2;
        }
        public override Expression<Func<TEntity, bool>> Expression
        {
            get
            {
                return specification1.Expression.Or(specification2.Expression);
            }
        }
    }
}