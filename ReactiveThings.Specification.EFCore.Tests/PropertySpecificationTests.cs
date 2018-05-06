using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.EFCore.Tests
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
        public void EntityFrameworkSpecificationTest(TestObject testObject, bool result)
        {
            var specification = new EF.PropertySpecification<TestObject, TestObject>(p => p.Property, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            EFCoreSpecificationTester.TestSpecification(specification, testObject, result);
        }

    }
}