using LinqKit;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace ReactiveThings.Specification.EF
{
    public class PropertyAnySpecification<TEntity, PropertyType> : Specification<TEntity>
    {
        private readonly Expression<Func<TEntity, IEnumerable<PropertyType>>> property;
        private readonly ISpecification<PropertyType> specification;

        public PropertyAnySpecification(Expression<Func<TEntity, IEnumerable<PropertyType>>> property, ISpecification<PropertyType> specification)
        {
            this.property = property;
            this.specification = specification;
        }

        public override Expression<Func<TEntity, bool>> Expression
        {
            get
            {
                var propertyExpression = specification.Expression;
                Expression<Func<TEntity, bool>> subPredicate = t => property.Invoke(t).Any(a => propertyExpression.Invoke(a));
                return subPredicate.Expand();
            }
        }
    }
}