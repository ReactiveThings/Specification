using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class NotSpecificationTests
    {
        public static IEnumerable<object[]> TestDataFactory()
        {
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = true,
                    },
                    false
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = false,
                    },
                    true
            };
        }
        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void IsSatisfiedByReturnsTrueWhenSpecifactionIsFalse(TestObject testObject, bool result)
        {
            var specification1 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty);

            var notSpecification = new NotSpecification<TestObject>(specification1);

            notSpecification.IsSatisfiedBy(testObject).ShouldBe(result);
        }
        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ExpressionReturnsTrueWhenSpecifactionIsFalse(TestObject testObject, bool result)
        {
            var specification1 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty);

            var notSpecification = new NotSpecification<TestObject>(specification1);

            ExpressionSpecificationTester.TestSpecification(notSpecification, testObject, result);

        }



    }
}