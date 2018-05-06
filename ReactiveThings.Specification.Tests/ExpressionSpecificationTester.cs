using Shouldly;
using System.Linq;

namespace ReactiveThings.Specification.Tests
{
    public static class ExpressionSpecificationTester
    {
        public static void TestSpecification(ISpecification<TestObject> specification, TestObject testObject, bool expectedResult)
        {
            var results = new[] { testObject }.Where(specification).SingleOrDefault();
            if (expectedResult)
            {
                results.ShouldNotBeNull();
            }
            else
            {
                results.ShouldBeNull();
            }
        }
    }
}