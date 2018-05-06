using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.EF.Tests
{
    public class PropertyWhenAllSpecificationTests
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
        public void EntityFrameworkSpecificationTest(TestObject testObject, bool result)
        {
            var specification = new AndSpecification<TestObject>(
                new Specification.EF.PropertyAnySpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty)), 
                new Specification.EF.PropertyAllSpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty)));
            EFSpecificationTester.TestSpecification(specification, testObject, result);
        }
    }

}