using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.Tests
{
    public class PropertyAnySpecificationTests
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
                                BooleanProperty = false
                            }
                        }
                    },
                    false
            };
            yield return new object[]
            {
                    new TestObject()
                    {
                        Collection = new[] {
                            new TestObject()
                            {
                                BooleanProperty = true
                            }
                        }
                    },
                    true
            };
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void IsSatisfiedByReturnsTrueWhenAnyObjectInCollectionSatisfiesExpression(TestObject testObject, bool expectedResult)
        {
            var specification = new PropertyAnySpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            specification.IsSatisfiedBy(testObject).ShouldBe(expectedResult);
        }

        [Theory]
        [MemberData(nameof(TestDataFactory))]
        public void ExpressionReturnsTrueWhenAnyObjectInCollectionSatisfiesExpression(TestObject testObject, bool expectedResult)
        {
            var specification = new PropertyAnySpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            ExpressionSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }
    }

}