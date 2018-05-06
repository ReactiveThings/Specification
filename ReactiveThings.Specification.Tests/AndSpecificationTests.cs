using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class AndSpecificationTests
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
                    false
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        BooleanProperty = true,
                        BooleanProperty1 = false,
                    },
                    false
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
        public void IsSatisfiedByReturnsTrueWhenAllSpecifactionsAreTrue(TestObject testObject, bool result)
        {
            var specification1 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty);
            var specification2 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty1);

            var andSpecification = new AndSpecification<TestObject>(specification1, specification2);

            andSpecification.IsSatisfiedBy(testObject).ShouldBe(result);
        }
        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ExpressionReturnsTrueWhenAllSpecifactionsAreTrue(TestObject testObject, bool result)
        {
            var specification1 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty);
            var specification2 = new ExpressionSpecification<TestObject>(p => p.BooleanProperty1);

            var andSpecification = new AndSpecification<TestObject>(specification1, specification2);

            ExpressionSpecificationTester.TestSpecification(andSpecification, testObject, result);

        }


    }
}