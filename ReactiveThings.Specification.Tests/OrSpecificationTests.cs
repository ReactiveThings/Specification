using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class OrSpecificationTests
    {
        public static IEnumerable<object[]> TestDataFactory()
        {
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = true,
                        BooleanProperty1 = true,
                    },
                    true
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = false,
                        BooleanProperty1 = true,
                    },
                    true
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = true,
                        BooleanProperty1 = false,
                    },
                    true
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = false,
                        BooleanProperty1 = false,
                    },
                    false
            };
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void IsSatisfiedByReturnsTrueWhenAnySpecifactionIsTrue(TestObject testObject, bool expectedResult)
        {
            var specification1 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty);
            var specification2 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty1);

            var orSpecification = new OrSpecification<TestObject>(specification1, specification2);

            orSpecification.IsSatisfiedBy(testObject).ShouldBe(expectedResult);
        }
        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ExpressionReturnsTrueWhenAnySpecifactionIsTrue(TestObject testObject, bool expectedResult)
        {
            var specification1 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty);
            var specification2 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty1);

            var orSpecification = new OrSpecification<TestObject>(specification1, specification2);

            ExpressionSpecificationTester.TestSpecification(orSpecification, testObject, expectedResult);

        }

    }
}