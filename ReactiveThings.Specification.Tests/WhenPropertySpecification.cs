using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class PropertySpecificationTests
    {
        public static IEnumerable<object[]> TestDataFactory()
        {
            yield return new object[]
            {
                new TestObject()
                {
                    Property = new TestObject()
                    {
                        BooleanProperty = true
                    }
                },
                true
            };
            yield return new object[]
            {
                new TestObject()
                    {
                        Property = new TestObject()
                        {
                            BooleanProperty = false
                        }
                    },
                false
            };
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ReturnsCorrectResultForLambdaExpression(TestObject testObject, bool expectedResult)
        {
            var specification = new PropertySpecification<TestObject, TestObject>(p => p.Property, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            specification.IsSatisfiedBy(testObject).ShouldBe(expectedResult);
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ExpressionReturnsCorrectResult(TestObject testObject, bool result)
        {
            var specification = new PropertySpecification<TestObject, TestObject>(p => p.Property, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            ExpressionSpecificationTester.TestSpecification(specification, testObject, result);
        }


    }
}