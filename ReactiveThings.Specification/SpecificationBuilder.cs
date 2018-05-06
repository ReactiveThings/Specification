
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReactiveThings.Specification
{
    public abstract class SpecificationBuilder<TEntity,TSpecificationBuilder> : ISpecification<TEntity> where TSpecificationBuilder : SpecificationBuilder<TEntity, TSpecificationBuilder>, new()
    {
        protected ISpecification<TEntity> specification = null;

        public bool IsEmpty => specification == null;

        public Expression<Func<TEntity, bool>> Expression
        {
            get
            {
                if (IsEmpty) return p => true;
                return specification.Expression;
            }
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return Expression.Compile().Invoke(entity);
        }

        protected virtual TSpecificationBuilder Where(Expression<Func<TEntity, bool>> expression)
        {
            And(new ExpressionSpecification<TEntity>(expression));
            return Builder();
        }

        protected virtual TSpecificationBuilder Where(ISpecification<TEntity> specification)
        {
            And(specification);
            return Builder();
        }

        protected virtual TSpecificationBuilder Where<TPropertySpecificationBuilder, TPropertyType>(Expression<Func<TEntity, TPropertyType>> property, Action<TPropertySpecificationBuilder> build)
            where TPropertySpecificationBuilder : SpecificationBuilder<TPropertyType, TPropertySpecificationBuilder>, new()
        {
            var builder = new TPropertySpecificationBuilder();
            build(builder);
            if (!builder.IsEmpty)
            {
                And(new PropertySpecification<TEntity, TPropertyType>(property, new ExpressionSpecification<TPropertyType>(builder.Expression)));
            }
            return Builder();
        }

        protected virtual TSpecificationBuilder Any<TPropertySpecificationBuilder, TPropertyType>(Expression<Func<TEntity, IEnumerable<TPropertyType>>> property, Action<TPropertySpecificationBuilder> build)
            where TPropertySpecificationBuilder : SpecificationBuilder<TPropertyType, TPropertySpecificationBuilder>, new()
        {
            var builder = new TPropertySpecificationBuilder();
            build(builder);
            if (!builder.IsEmpty)
            {
                And(new PropertyAnySpecification<TEntity, TPropertyType>(property, new ExpressionSpecification<TPropertyType>(builder.Expression)));
            }
            return Builder();
        }

        protected virtual TSpecificationBuilder All<TPropertySpecificationBuilder, TPropertyType>(Expression<Func<TEntity, IEnumerable<TPropertyType>>> property, Action<TPropertySpecificationBuilder> build)
            where TPropertySpecificationBuilder : SpecificationBuilder<TPropertyType, TPropertySpecificationBuilder>, new()
        {
            var builder = new TPropertySpecificationBuilder();
            build(builder);
            if (!builder.IsEmpty)
            {
                And(new PropertyAllSpecification<TEntity, TPropertyType>(property, new ExpressionSpecification<TPropertyType>(builder.Expression)));
            }
            return Builder();
        }

        public void And(ISpecification<TEntity> specification)
        {
            if (IsEmpty)
            {
                this.specification = specification;
            }
            else
            {
                this.specification = new AndSpecification<TEntity>(this.specification, specification);
            }
        }

        public virtual TSpecificationBuilder Or(Action<TSpecificationBuilder> build)
        {
            var specification = CreateSpecification(build);
            if (IsEmpty) throw new Exception("Connot apply or operator to empty specification");
            this.specification = new OrSpecification<TEntity>(this.specification, specification);

            return Builder();
        }

        public virtual TSpecificationBuilder OrNot(Action<TSpecificationBuilder> build)
        {
            var specification = CreateSpecification(build);
            if (IsEmpty) throw new Exception("Connot apply or not operator to empty specification");
            this.specification = new OrSpecification<TEntity>(this.specification, new NotSpecification<TEntity>(specification));

            return Builder();
        }

        public virtual TSpecificationBuilder Or(params Action<TSpecificationBuilder>[] buildActions)
        {
            var orSpecification = buildActions.Select(CreateSpecification).Aggregate((s1, s2) => new OrSpecification<TEntity>(s1, s2));

            return Where(orSpecification);
        }

        public virtual TSpecificationBuilder OrNot(params Action<TSpecificationBuilder>[] buildActions)
        {
            var orSpecification = buildActions.Select(CreateSpecification).Aggregate((s1, s2) => new OrSpecification<TEntity>(s1, s2));
            return Where(new NotSpecification<TEntity>(orSpecification));
        }

        public virtual TSpecificationBuilder Not(Action<TSpecificationBuilder> build)
        {
            var specification = CreateSpecification(build);

            return Where(new NotSpecification<TEntity>(specification));
        }

        protected virtual TSpecificationBuilder Builder()
        {
            return this as TSpecificationBuilder;
        }

        private static ISpecification<TEntity> CreateSpecification(Action<TSpecificationBuilder> build)
        {
            var builder = new TSpecificationBuilder();
            build(builder);
            return builder;
        }
    }
}