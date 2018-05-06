using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.EFCore.Tests
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
        public void EntityFrameworkSpecificationTest(TestObject testObject, bool result)
        {
            var specification = new EF.PropertyAnySpecification<TestObject, TestObject>(p => p.Collection, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));
            EFCoreSpecificationTester.TestSpecification(specification, testObject, result);
        }
    }

}