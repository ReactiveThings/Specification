
using System;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public class PropertySpecification<TEntity, PropertyType> : Specification<TEntity>
    {
        private readonly Expression<Func<TEntity, PropertyType>> property;
        private readonly ISpecification<PropertyType> specification;
        public PropertySpecification(Expression<Func<TEntity, PropertyType>> property, ISpecification<PropertyType> specification)
        {
            this.property = property;
            this.specification = specification;
        }

        public override Expression<Func<TEntity, bool>> Expression
        {
            get
            {
                var propertyExpression = specification.Expression;
                Expression<Func<TEntity, bool>> subPredicate = t => property.Compile().Invoke(t) != null && propertyExpression.Compile().Invoke(property.Compile().Invoke(t));
                return subPredicate;
            }
        }
    }
}