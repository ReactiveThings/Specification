using System.Linq;

namespace ReactiveThings.Specification
{
    public static class Extensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> queryable, ISpecification<T> specification)
        {
            return queryable.Where(specification.Expression);
        }
    }
}