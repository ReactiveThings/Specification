using System.Collections.Generic;
using System.Linq;

namespace ReactiveThings.Specification
{
    public static class Extensions
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> queryable, ISpecification<T> specification)
        {
            return queryable.Where(specification.Expression.Compile());
        }
    }
}