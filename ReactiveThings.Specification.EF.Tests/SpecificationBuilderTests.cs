using System;
using System.Collections.Generic;
using Xunit;

namespace ReactiveThings.Specification.EF.Tests
{
    public class TestObjectSpecification : SpecificationBuilder<TestObject,TestObjectSpecification>
    {
        public TestObjectSpecification BooleanProperty(bool value)
        {
            return Where(p => p.BooleanProperty == value);
        }


        public TestObjectSpecification ByProperty(Action<TestObjectSpecification> build)
        {
            return Where(p => p.Property, build);
        }

        public TestObjectSpecification CollectionAll(Action<TestObjectSpecification> build)
        {
            return All(p => p.Collection, build);
        }

        public TestObjectSpecification CollectionAny(Action<TestObjectSpecification> build)
        {
            return Any(p => p.Collection, build);
        }
    }

    public class SpecificationBuilderTests
    {
        [Fact]
        public void AllowsPropertyToBeFilteredBySpecification()
        {
            var testObject = new TestObject()
            {
                Property = new TestObject()
                {
                    BooleanProperty = true
                }
            };

            var specification = new TestObjectSpecification()
                .ByProperty(p => p.BooleanProperty(true));

            var expectedResult = true;

            EFSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void AllowsCollectionToBeFilteredBySpecificationWithAnyOperator()
        {
            var testObject = new TestObject()
            {
                Collection = new List<TestObject>()
                {
                    new TestObject(){BooleanProperty = true}
                }
            };

            var specification = new TestObjectSpecification()
                .CollectionAny(p => p.BooleanProperty(true));

            var expectedResult = true;

            EFSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void AllowsCollectionToBeFilteredBySpecificationWithAllOperator()
        {
            var testObject = new TestObject()
            {
                Collection = new List<TestObject>()
                {
                    new TestObject(){BooleanProperty = true},
                    new TestObject(){BooleanProperty = true}
                }
            };

            var specification = new TestObjectSpecification()
                .CollectionAny(p => p.BooleanProperty(true))
                .CollectionAll(p => p.BooleanProperty(true));

            var expectedResult = true;

            EFSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void WhenSpecificationForPropertyIsEmptyOmitSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification()
                .ByProperty(p => {});

            var expectedResult = true;

            EFSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void WhenSpecificationForCollectionAnyIsEmptyOmitSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification()
                .CollectionAny(p => { });

            var expectedResult = true;

            EFSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }

        [Fact]
        public void WhenSpecificationForCollectionAllIsEmptyOmitSpecification()
        {
            var testObject = new TestObject();

            var specification = new TestObjectSpecification()
                .CollectionAll(p => { });

            var expectedResult = true;

            EFSpecificationTester.TestSpecification(specification, testObject, expectedResult);
        }
    }
}
