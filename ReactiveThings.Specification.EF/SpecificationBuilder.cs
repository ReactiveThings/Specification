using System;
using System.Collections.Generic;

namespace ReactiveThings.Specification.EF
{
    public abstract class SpecificationBuilder<TEntity, TSpecificationBuilder> : Specification.SpecificationBuilder<TEntity, TSpecificationBuilder> where TSpecificationBuilder : SpecificationBuilder<TEntity, TSpecificationBuilder>, new()
    {
        protected override TSpecificationBuilder Where<TPropertySpecification, TPropertyType>(System.Linq.Expressions.Expression<Func<TEntity, TPropertyType>> property, Action<TPropertySpecification> build)
        {
            var builder = new TPropertySpecification();
            build(builder);
            if (!builder.IsEmpty)
            {
                And(new PropertySpecification<TEntity, TPropertyType>(property, new ExpressionSpecification<TPropertyType>(builder.Expression)));
            }
            return Builder();
        }

        protected override TSpecificationBuilder Any<TPropertySpecificationBuilder, TPropertyType>(System.Linq.Expressions.Expression<Func<TEntity, IEnumerable<TPropertyType>>> property, Action<TPropertySpecificationBuilder> build)
        {
            var builder = new TPropertySpecificationBuilder();
            build(builder);
            if (!builder.IsEmpty)
            {
                And(new PropertyAnySpecification<TEntity, TPropertyType>(property, new ExpressionSpecification<TPropertyType>(builder.Expression)));
            }
            return Builder();
        }

        protected override TSpecificationBuilder All<TPropertySpecificationBuilder, TPropertyType>(System.Linq.Expressions.Expression<Func<TEntity, IEnumerable<TPropertyType>>> property, Action<TPropertySpecificationBuilder> build)
        {
            var builder = new TPropertySpecificationBuilder();
            build(builder);
            if (!builder.IsEmpty)
            {
                And(new PropertyAllSpecification<TEntity, TPropertyType>(property, new ExpressionSpecification<TPropertyType>(builder.Expression)));
            }
            return Builder();
        }
    }
}
