using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.EF.Tests
{
    public class WhenPropertySpecificationTests
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
            var specification = new Specification.EF.PropertySpecification<TestObject, TestObject>(p => p.Property, new ExpressionSpecification<TestObject>(p => p.BooleanProperty));

            EFSpecificationTester.TestSpecification(specification, testObject, result);
        }

    }
}