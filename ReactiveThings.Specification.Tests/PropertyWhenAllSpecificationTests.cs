using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class PropertyAllSpecificationTests
    {
        public static IEnumerable<object[]> TestDataFactory()
        {
            yield return new object[]
            {
                    new TestObject()
                    {
                        Collection = new[] {
                            new TestObject()
                            {
                                BooleanProperty = true
                            },
                            new TestObject()
                            {
                                BooleanProperty = true
                            }
                        }
                    },
                    true
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        Collection = new[] {
                            new TestObject()
                            {
                                BooleanProperty = true
                            },
                            new TestObject()
                            {
                                BooleanProperty = false
                            }
                        }
                    },
                    false
            };
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void IsSatisfiedByReturnsTrueWhenAllObjectInCollectionSatisfiesExpression(TestObject testObject, bool expectedResult)
        {
            var specification = new PropertyAllSpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            specification.IsSatisfiedBy(testObject).ShouldBe(expectedResult);
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ExpressionReturnsTrueWhenAllObjectInCollectionSatisfiesExpression(TestObject testObject, bool result)
        {
            var specification = new PropertyAllSpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            ExpressionSpecificationTester.TestSpecification(specification, testObject, result);

        }

    }

}